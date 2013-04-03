using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace datasift
{
    /// <summary>
    /// The implementation of the HTTP StreamConsumer.
    /// </summary>
    class StreamConsumer_Http : StreamConsumer
    {
        /// <summary>
        /// Construct a consumer for the given definition.
        /// </summary>
        /// <param name="user">The User object which is creating this consumer.</param>
        /// <param name="definition">The Definition to be consumed.</param>
        /// <param name="event_handler">The object that will receive events.</param>
        public StreamConsumer_Http(User user, Definition definition, IEventHandler event_handler)
            : base(user, definition, event_handler)
        {
        }

        /// <summary>
        /// Construct a consumer for the given array of stream hashes.
        /// </summary>
        /// <param name="user">The User object which is creating this consumer.</param>
        /// <param name="hashes">The array of stream hashes to be consumed.</param>
        /// <param name="event_handler">The object that will receive events.</param>
        public StreamConsumer_Http(User user, string[] hashes, IEventHandler event_handler)
            : base(user, hashes, event_handler)
        {
        }

        /// <summary>
        /// Called by the base class when it wants the consumer to start.
        /// </summary>
        /// <param name="auto_reconnect">True to reconnect should the connection get dropped.</param>
        protected override void onStart(bool auto_reconnect = true)
        {
            bool firstConnection = true;
            int connectionDelay = 0;
            while ((firstConnection || auto_reconnect) && isRunning(true))
            {
                firstConnection = false;

                // Do we need to wait before trying to reconnect?
                if (connectionDelay > 0)
                {
                    Thread.Sleep(connectionDelay * 1000);
                }

                byte[] buffer = new byte[65536];
                HttpWebResponse response;
                WebExceptionStatus status;

                try
                {
                    // Build the request.
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUrl());
                    request.Timeout = 65000;
                    request.ReadWriteTimeout = 65000;
                    request.Headers["Auth"] = getAuthHeader();
                    request.UserAgent = getUserAgent();

                    // Get the response.
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException e)
                {
                    // An error occurred, but we handle the response with the same code below.
                    response = (HttpWebResponse)e.Response;
                    status = e.Status;
                }
                catch (Exception e)
                {
                    // Have we exhausted the recommended retries?
                    if (connectionDelay < 16)
                    {
                        // No. Increment the retry delay by a second and tell the user about the problem.
                        connectionDelay += 1;
                        onWarning("Connection failed (" + e.Message + "), retrying in " + connectionDelay + " second" + (connectionDelay == 1 ? "" : "s"));
                        // Try again.
                        continue;
                    }

                    // No more retries. Tell the user and break out of the reconnect loop.
                    onWarning("Connection failed (" + e.Message + "), no more retries");
                    break;
                }

                int statusCode = (response == null ? -1 : (int)response.StatusCode);

                if (statusCode == 200)
                {
                    // Yay, connected. Reset the delay, tell the user we're connected and start reading the stream.
                    connectionDelay = 0;
                    onConnect();
                    readStream(new StreamReader(response.GetResponseStream()));
                }
                else if (statusCode == 404)
                {
                    // Unknown hash.
                    onError("Hash not found");
                    break;
                }
                else if (statusCode >= 400 && statusCode < 500 && statusCode != 420)
                {
                    // A 4xx (excluding 420) response should contain an error message in JSON.
                    StreamReader response_stream = new StreamReader(response.GetResponseStream());
                    string json_data = "init";
                    while (json_data.Length <= 4)
                    {
                        json_data = response_stream.ReadLine();
                    }
                    JSONdn json = new JSONdn(json_data);
                    if (json.has("message"))
                    {
                        onError(json.getStringVal("message"));
                    }
                    else
                    {
                        onError("Unhandled error code: " + statusCode.ToString() + " " + json_data);
                    }
                    // Break out of the reconnect loop.
                    break;
                }
                else
                {
                    // All other response codes follow the recommended retry pattern for server-side errors.
                    if (connectionDelay == 0)
                    {
                        connectionDelay = 10;
                    }
                    else if (connectionDelay < 320)
                    {
                        connectionDelay *= 2;
                    }
                    else
                    {
                        // We've hit the retry limit, tell the user and break out of the reconnect loop.
                        onError((statusCode == -1 ? "Connection failed" : "Received " + statusCode.ToString() + " response") + ", no more retries");
                        break;
                    }
                    // Tell the user that we're retrying.
                    onWarning((statusCode == -1 ? "Connection failed" : "Received " + statusCode.ToString() + " response") + ", retrying in " + connectionDelay + " seconds");
                }
            }

            // Tell the user we've disconnected.
            onDisconnect();
        }

        /// <summary>
        /// Read the stream.
        /// </summary>
        /// <param name="reader">The StreamReader from the connection.</param>
        private void readStream(StreamReader reader)
        {
            string line = String.Empty;
            // Before we read each line, check that we're still supposed to be running.
            while (isRunning(true))
            {
                // Empty the line.
                line = String.Empty;

                // Read a line, ignoring exceptions - we check for an empty string below.
                try
                {
                    line = reader.ReadLine();
                }
                catch (Exception)
                {
                }

                if (line == null || line.Length == 0)
                {
                    // An error occurred, break out of the loop. This will cause the stream to disconnect.
                    break;
                }
                else
                {
                    // Tell the user we've received something.
                    onData(line);
                }
            }
        }
    }
}
