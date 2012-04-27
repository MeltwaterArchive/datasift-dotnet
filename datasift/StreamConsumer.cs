using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    public abstract class StreamConsumer
    {
        public static StreamConsumer factory(User user, string type, Definition definition, IEventHandler event_handler)
        {
            switch (type)
            {
                case "http":
                    return new StreamConsumer_Http(user, definition, event_handler);

                default:
                    throw new InvalidDataException("Consumer type \"" + type + "\" is unknown");
            }
        }

        public static StreamConsumer factory(User user, string type, string[] hashes, IEventHandler event_handler)
        {
            switch (type)
            {
                case "http":
                    return new StreamConsumer_Http(user, hashes, event_handler);

                default:
                    throw new InvalidDataException("Consumer type \"" + type + "\" is unknown");
            }
        }

        public enum State { Stopped = 0, Starting, Running, Stopping };

        private User m_user = null;
        private List<string> m_hashes = new List<string>();
        private IEventHandler m_event_handler = null;
        private State m_state = State.Stopped;

        public StreamConsumer(User user, Definition definition, IEventHandler event_handler)
        {
            init(user, event_handler);
            m_hashes.Add(definition.getHash());
        }

        public StreamConsumer(User user, string[] hashes, IEventHandler event_handler)
        {
            init(user, event_handler);
            foreach (string hash in hashes)
            {
                m_hashes.Add(hash);
            }
        }

        public void init(User user, IEventHandler event_handler)
        {
            m_user = user;
            m_event_handler = event_handler;
            m_state = State.Stopped;
        }

        public void consume(bool auto_reconnect = true)
        {
            m_state = State.Starting;
            onStart(auto_reconnect);
        }

        public void stop()
        {
            if (m_state != State.Running)
            {
                throw new InvalidDataException("Consumer state must be RUNNING before it can be stopped");
            }
            m_state = State.Stopping;
        }

        protected string getUrl()
        {
            if (m_hashes.Count == 1)
            {
                return "http://" + User.STREAM_BASE_URL + "multi?hashes=" + String.Join(",", m_hashes.ToArray());
            }
            else
            {
                return "http://" + User.STREAM_BASE_URL + m_hashes[0];
            }
        }

        protected string getAuthHeader()
        {
            return m_user.getUsername() + ":" + m_user.getApiKey();
        }

        protected string getUserAgent()
        {
            return User.USER_AGENT;
        }

        public State getState()
        {
            return m_state;
        }

        public bool isRunning(bool allowStarting = false)
        {
            return (allowStarting && m_state == State.Starting) || m_state == State.Running;
        }

        protected void onConnect()
        {
            m_state = State.Running;
            m_event_handler.onConnect(this);
        }

        protected void onData(string json)
        {
            JSONdn data = new JSONdn(json);
            if (data.has("status"))
            {
                if (data.has("tick"))
                {
                    // Ignore ticks
                }
                else switch (data.getStringVal("status"))
                {
                    case "failure":
                    case "error":
                        onError(data.getStringVal("message"));
                        break;

                    case "warning":
                        onWarning(data.getStringVal("message"));
                        break;

                    default:
                        onWarning("Unhandled status message: \"" + data.getStringVal("status") + "\"");
                        break;
                }
            }
            else if (data.has("hash"))
            {
                if (data.has("data.deleted"))
                {
                    m_event_handler.onDeleted(this, new Interaction(data.getJVal("data")), data.getStringVal("hash"));
                }
                else
                {
                    m_event_handler.onInteraction(this, new Interaction(data.getJVal("data")), data.getStringVal("hash"));
                }
            }
            else if (data.has("interaction"))
            {
                if (data.has("deleted"))
                {
                    m_event_handler.onDeleted(this, (Interaction)data, m_hashes[0]);
                }
                else
                {
                    m_event_handler.onInteraction(this, (Interaction)data, m_hashes[0]);
                }
            }
            else
            {
                onError("Unhandled data received: " + json);
            }
        }

        protected void onWarning(string message)
        {
            m_event_handler.onWarning(this, message);
        }

        protected void onError(string message)
        {
            m_event_handler.onError(this, message);
        }

        protected void onDisconnect()
        {
            m_event_handler.onDisconnect(this);
        }

        protected abstract void onStart(bool auto_reconnect = true);
    }
}
