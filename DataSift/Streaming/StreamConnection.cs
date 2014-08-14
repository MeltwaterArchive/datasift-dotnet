using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;

namespace DataSift.Streaming
{
    public class StreamConnection : IStreamConnection
    {
        public event EventHandler Opened;
        public event EventHandler Closed;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<SuperSocket.ClientEngine.ErrorEventArgs> Error;

        private WebSocket _websocket = null;
        private string _url;
        private string _userAgent;

        public DateTime LastActiveTime { get { return _websocket.LastActiveTime; } }

        internal StreamConnection(string url)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            _userAgent = "DataSift/v1 Dotnet/v" + version.ToString();
            _url = url;

            Setup();

        }


        void _websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (MessageReceived != null)
                MessageReceived(sender, e);
        }

        void _websocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            if (Error != null)
                Error(sender, e);
        } 

        void _websocket_Closed(object sender, EventArgs e)
        {
            if (Closed != null)
                Closed(sender, e);
        }

        void _websocket_Opened(object sender, EventArgs e)
        {
            if (Opened != null)
                Opened(sender,e);
        }

        private void Setup()
        {
            _websocket = new WebSocket(_url, userAgent: _userAgent);
            _websocket.EnableAutoSendPing = true;
            _websocket.AutoSendPingInterval = 30;
            _websocket.Opened += _websocket_Opened;
            _websocket.Closed += _websocket_Closed;
            _websocket.Error += _websocket_Error;
            _websocket.MessageReceived += _websocket_MessageReceived;
        }

        public void Reconnect()
        {
            Close();
            Setup();
            Open();
        }

        public void Close()
        {
            if (_websocket.State == WebSocketState.Open)
            {
                _websocket.Close();
            }
        }

        public void Open()
        {
            _websocket.Open();
        }

        public void Send(string message)
        {
            _websocket.Send(message);
        }
    }
}
