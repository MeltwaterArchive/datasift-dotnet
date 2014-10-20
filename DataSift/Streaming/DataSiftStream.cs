using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using SuperSocket.ClientEngine;
using WebSocket4Net;
using Newtonsoft.Json;
using DataSift.Enum;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using System.Timers;
using System.Threading;
using System.Diagnostics;

namespace DataSift.Streaming
{
    public class DataSiftStream
    {
        private List<string> _subscriptions = new List<string>();
        private Dictionary<string, OnMessageHandler> _messageHandlers = new Dictionary<string, OnMessageHandler>();
        private Dictionary<string, OnSubscribedHandler> _subscribedHandlers  = new Dictionary<string, OnSubscribedHandler>();
        private Dictionary<string, OnMessageHandler> _deleteHandlers = new Dictionary<string, OnMessageHandler>();

        private IStreamConnection _connection;
        private DataSiftClient.GetStreamConnectionDelegate _getConnection;

        private string _domain;
        private System.Timers.Timer _deadConnectionChecker;
        private bool _autoReconnect;
        private bool _intentionallyClosed;
        private bool _reconnecting;

        private int _connectRetries = 0;
        private int _connectRetriesMax = 7;
        private DateTime _connectedTime = DateTime.MinValue;

        #region Public Events

        public delegate void OnConnectHandler();
        public event OnConnectHandler OnConnect;

        public delegate void OnMessageHandler(string hash, dynamic message);
        public event OnMessageHandler OnMessage;

        public event OnMessageHandler OnDelete;

        public delegate void OnSubscribedHandler(string hash);
        public event OnSubscribedHandler OnSubscribed;

        public delegate void OnDataSiftMessageHandler(DataSiftMessageStatus status, string message);
        public event OnDataSiftMessageHandler OnDataSiftMessage;

        public delegate void OnErrorHandler(StreamAPIException ex);
        public event OnErrorHandler OnError;

        public delegate void OnClosedHandler();
        public event OnClosedHandler OnClosed;

        #endregion

        #region Public Methods

        public DataSiftStream(DataSiftClient.GetStreamConnectionDelegate connectionCreator, string domain, bool autoReconnect = true)
        {
            _domain = domain;
            _autoReconnect = autoReconnect;

            if (connectionCreator == null)
                _getConnection = GetConnectionDefault;
            else
                _getConnection = connectionCreator;
        }

        internal void Connect(string username, string apikey, bool secure = true)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
             
            var protocol = (secure) ? "wss" : "ws";
            var url = String.Format("{0}://{1}/multi?statuses=true&username={2}&api_key={3}", protocol, _domain, username, apikey);

            Trace.TraceInformation("Connecting to " + url);

            _connection = GetConnection(url);
            _connection.Opened += _connection_Opened;
            _connection.Error += _connection_Error;
            _connection.Closed += _connection_Closed;
            _connection.MessageReceived += _connection_MessageReceived;
            _connection.Open();
        }

        public void Close()
        {
            _intentionallyClosed = true;
            _connection.Close();
        }

        public void Subscribe(string hash, OnMessageHandler messageHandler = null, OnSubscribedHandler subscribedHandler = null, OnMessageHandler deleteHandler = null)
        {
            Contract.Requires<ArgumentNullException>(hash != null);
            Contract.Requires<ArgumentException>(hash.Trim().Length > 0);
            Contract.Requires<ArgumentException>(Constants.STREAM_HASH_FORMAT.IsMatch(hash), Messages.INVALID_STREAM_HASH);

            if(!_subscriptions.Contains(hash))
                _subscriptions.Add(hash);

            if(messageHandler != null)
            {
                if (_messageHandlers.ContainsKey(hash))
                    _messageHandlers[hash] = messageHandler;
                else
                    _messageHandlers.Add(hash, messageHandler);
            }

            if (subscribedHandler != null)
            {
                if (_subscribedHandlers.ContainsKey(hash))
                    _subscribedHandlers[hash] = subscribedHandler;
                else
                    _subscribedHandlers.Add(hash, subscribedHandler);
            }

            if (deleteHandler != null)
            {
                if (_deleteHandlers.ContainsKey(hash))
                    _deleteHandlers[hash] = deleteHandler;
                else
                    _deleteHandlers.Add(hash, deleteHandler);
            }

            Trace.TraceInformation("Subscribing to stream: " + hash);

            var message = new { action = "subscribe", hash = hash };
            _connection.Send(JsonConvert.SerializeObject(message));
        }

        public void Unsubscribe(string hash)
        {
            Contract.Requires<ArgumentNullException>(hash != null);
            Contract.Requires<ArgumentException>(hash.Trim().Length > 0);
            Contract.Requires<ArgumentException>(Constants.STREAM_HASH_FORMAT.IsMatch(hash), Messages.INVALID_STREAM_HASH);

            if(_subscriptions.Contains(hash))
                _subscriptions.Remove(hash);

            if (_messageHandlers.ContainsKey(hash))
                _messageHandlers.Remove(hash);

            if (_subscribedHandlers.ContainsKey(hash))
                _subscribedHandlers.Remove(hash);

            if (_deleteHandlers.ContainsKey(hash))
                _deleteHandlers.Remove(hash);

            Trace.TraceInformation("Unsubscribing from stream: " + hash);

            var message = new { action = "unsubscribe", hash = hash };
            _connection.Send(JsonConvert.SerializeObject(message));
        }

        #endregion


        #region Mocking / Faking

        private IStreamConnection GetConnectionDefault(string url)
        {
            return new StreamConnection(url);
        }

        internal IStreamConnection GetConnection(string url)
        {
            return _getConnection(url);
        }

        #endregion

        #region Internal Methods

        private void _connection_Error(object sender, ErrorEventArgs e)
        {
            _reconnecting = false;
            Trace.TraceError("Connection error: " + e.Exception.StackTrace);

            if (_autoReconnect)
                Reconnect(true, true, false);
            else
                FireError(e.Exception);
        }

        private void FireError(Exception e)
        {
            if (OnError != null)
                OnError(new StreamAPIException("A connection error has occurred", e));
        }

        private void _connection_Closed(object sender, EventArgs e)
        {
            Trace.TraceWarning("Connection was closed.");

            if (!_intentionallyClosed)
            {
                if (_autoReconnect)
                    Reconnect(true, false, true);
                else
                    FireClose();
            }
            else
                _intentionallyClosed = false;
        }

        private void FireClose()
        {
            if (OnClosed != null)
                OnClosed();
        }

        private void _connection_Opened(object sender, EventArgs e)
        {
            Trace.TraceInformation("Connection opened.");
            _connectedTime = DateTime.Now;
            _connectRetries = 0;

            if(_reconnecting)
            {
                // Subscribe to any subscriptions may be some if a reconnect)
                foreach(var hash in _subscriptions)
                {
                    this.Subscribe(hash);
                }
            }
            else
            {
                // Only fire connect event if not a reconnect
                if (OnConnect != null)
                    OnConnect();
            }

            _reconnecting = false;

            // Start timer to check for dead connections
            _deadConnectionChecker = new System.Timers.Timer(10000);
            _deadConnectionChecker.Elapsed += CheckForDeadConnection;
            _deadConnectionChecker.Start();
        }

        // If we have had no pings or pongs for 60 seconds, assume connection is dead and we need to reconnect
        private void CheckForDeadConnection(object sender, ElapsedEventArgs e)
        {
            // If only just connected or in process of reconnecting ignore
            if (_reconnecting || (_connectedTime > (DateTime.Now.Subtract(new TimeSpan(0, 0, 60)))))
                return;

            if(_connection.LastActiveTime < (DateTime.Now.Subtract(new TimeSpan(0,0,60))))
            {
                Trace.TraceWarning("Connection appears dead - attempting reconnection.");
                Reconnect(false, false, false);
            }
        }

        private void Reconnect(bool withBackoff, bool fromError, bool fromClose)
        {
            if(_autoReconnect && !_reconnecting)
            {
                _deadConnectionChecker.Stop();

                if (!withBackoff)
                {
                    Trace.TraceInformation("Reconnecting to DataSift, without delay.");

                    _reconnecting = true;
                    _connection.Reconnect();
                }
                else
                {
                    if (_connectRetries >= _connectRetriesMax)
                    {
                        Trace.TraceWarning("Reached maximum number of reconnection retries: " + _connectRetriesMax.ToString());

                        if (fromError)
                            FireError(new StreamAPIException("Failed to reconnect to DataSift after multiple attempts. Try inspecting the application trace or disabling autoReconnect to investigate the issue.", new ApplicationException()));
                        else if (fromClose)
                            FireClose();
                    }
                    else
                    {
                        int wait = (int)(10 * (Math.Pow(2, _connectRetries)));
                        _connectRetries++;
                        _reconnecting = true;

                        Trace.TraceInformation("Reconnecting to DataSift, with backoff (in seconds): " + wait.ToString());

                        Thread.Sleep(wait * 1000);
                        _connection.Reconnect();
                    }
                }
            }
        }  


        private void _connection_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var message = APIHelpers.DeserializeResponse(e.Message);

            if (APIHelpers.HasAttr(message, "reconnect"))
            {
                // Server has asked clients to reconnect
                Reconnect(false, false, false);
            }
            else if (APIHelpers.HasAttr(message, "status"))
            {
                DataSiftMessageStatus status;

                if (System.Enum.TryParse<DataSiftMessageStatus>(message.status, true, out status))
                {
                    // Special case when subscription has succeeded
                    if (status == DataSiftMessageStatus.Success && APIHelpers.HasAttr(message, "hash"))
                    {
                        // Fire message at subscription level
                        if (_subscribedHandlers.ContainsKey(message.hash))
                            _subscribedHandlers[message.hash](message.hash);

                        // Fire message at connection level
                        if (OnSubscribed != null)
                            OnSubscribed(message.hash);
                    }
                    else
                    {
                        if (OnDataSiftMessage != null)
                            OnDataSiftMessage(status, message.message);
                    }
                }
            }
            else if (APIHelpers.HasAttr(message, "hash"))
            {
                // Check for deletes
                if (APIHelpers.HasAttr(message, "data"))
                {
                    if(APIHelpers.HasAttr(message.data, "deleted"))
                    {
                        // Fire delete at subscription level
                        if (_deleteHandlers.ContainsKey(message.hash))
                            _deleteHandlers[message.hash](message.hash, message.data);

                        // Fire delete at connection level
                        if (OnDelete != null)
                            OnDelete(message.hash, message.data);

                        return;
                    }
                }

                // Otherwise normal interaction

                // Fire message at subscription level
                if (_messageHandlers.ContainsKey(message.hash))
                    _messageHandlers[message.hash](message.hash, message.data);

                // Fire message at connection level
                if (OnMessage != null)
                    OnMessage(message.hash, message.data);
            }


        }

        #endregion

    }
}
