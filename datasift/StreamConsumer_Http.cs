using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace datasift
{
    class StreamConsumer_Http : StreamConsumer
    {
        public StreamConsumer_Http(User user, Definition definition, IEventHandler event_handler)
            : base(user, definition, event_handler)
        {
        }

        public StreamConsumer_Http(User user, string[] hashes, IEventHandler event_handler)
            : base(user, hashes, event_handler)
        {
        }

        protected override void onStart(bool auto_reconnect = true)
        {
            bool firstConnection = true;
            int connectionDelay = 0;
            while ((firstConnection || auto_reconnect) && isRunning(true))
            {
                firstConnection = false;
                if (connectionDelay > 0)
                {
                    Thread.Sleep(connectionDelay * 1000);
                }

                byte[] buffer = new byte[65536];
                HttpWebResponse response;

                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUrl());
                    request.Headers["Auth"] = getAuthHeader();
                    request.UserAgent = getUserAgent();
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException e)
                {
                    response = (HttpWebResponse)e.Response;
                }
                catch (Exception e)
                {
                    if (connectionDelay < 16)
                    {
                        connectionDelay += 1;
                        onWarning("Connection failed (" + e.Message + "), retrying in " + connectionDelay + " second" + (connectionDelay == 1 ? "" : "s"));
                        continue;
                    }
                    onWarning("Connection failed (" + e.Message + "), no more retries");
                    break;
                }

                int statusCode = (int)response.StatusCode;

                if (statusCode == 200)
                {
                    connectionDelay = 0;
                    onConnect();
                    readStream(new StreamReader(response.GetResponseStream()));
                }
                else if (statusCode == 404)
                {
                    onError("Hash not found");
                    break;
                }
                else if (statusCode >= 400 && statusCode < 500 && statusCode != 420)
                {
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
                    break;
                }
                else
                {
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
                        onError("Received " + statusCode.ToString() + " response, no more retries");
                        break;
                    }
                    onWarning("Received " + statusCode.ToString() + " response, retrying in " + connectionDelay + " seconds");
                }
            }
            onDisconnect();
        }

        private void readStream(StreamReader reader)
        {
            string line = "";
            while (isRunning(true))
            {
                try
                {
                    line = reader.ReadLine();
                }
                catch (Exception)
                {
                }

                if (line.Length == 0)
                {
                    break;
                }
                else
                {
                    onData(line);
                }
            }
        }
    }
}
