using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    /// <summary>
    /// The abstract base class for type-specific StreamConsumer
    /// implementations.
    /// </summary>
    public abstract class StreamConsumer
    {
        /// <summary>
        /// A factory method for creating a consumer from a Definition object.
        /// </summary>
        /// <param name="user">The user object that's creating this consumer.</param>
        /// <param name="type">The consumer type.</param>
        /// <param name="definition">The Definition to be consumed.</param>
        /// <param name="event_handler">An object that implements IEventHandler which will receive the events.</param>
        /// <returns>An object of a type derived from this class.</returns>
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

        /// <summary>
        /// A factory method for creating a consumer from an array of hashes.
        /// </summary>
        /// <param name="user">The user object that's creating this consumer.</param>
        /// <param name="type">The consumer type.</param>
        /// <param name="hashes">An array of stream hashes.</param>
        /// <param name="event_handler">An object that implements IEventHandler which will receive the events.</param>
        /// <returns>An object of a type derived from this class.</returns>
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

        /// <summary>
        /// An enum of possible states for a consumer.
        /// </summary>
        public enum State { Stopped = 0, Starting, Running, Stopping };

        /// <summary>
        /// The User object that created this consumer.
        /// </summary>
        private User m_user = null;

        /// <summary>
        /// The hashes being consumed by this consumer.
        /// </summary>
        private List<string> m_hashes = new List<string>();

        /// <summary>
        /// The object that will receive events from this consumer.
        /// </summary>
        private IEventHandler m_event_handler = null;

        /// <summary>
        /// The current state of this consumer.
        /// </summary>
        private State m_state = State.Stopped;

        /// <summary>
        /// Construct a consumer for the given definition.
        /// </summary>
        /// <param name="user">The User object which is creating this consumer.</param>
        /// <param name="definition">The Definition to be consumed.</param>
        /// <param name="event_handler">The object that will receive events.</param>
        public StreamConsumer(User user, Definition definition, IEventHandler event_handler)
        {
            init(user, event_handler);
            m_hashes.Add(definition.getHash());
        }

        /// <summary>
        /// Construct a consumer for the given array of stream hashes.
        /// </summary>
        /// <param name="user">The User object which is creating this consumer.</param>
        /// <param name="hashes">The array of stream hashes to be consumed.</param>
        /// <param name="event_handler">The object that will receive events.</param>
        public StreamConsumer(User user, string[] hashes, IEventHandler event_handler)
        {
            init(user, event_handler);
            foreach (string hash in hashes)
            {
                m_hashes.Add(hash);
            }
        }

        /// <summary>
        /// Initialise this consumer. This is used by both constructors.
        /// </summary>
        /// <param name="user">The user object which is creating this consumer.</param>
        /// <param name="event_handler">The object that will receive events.</param>
        public void init(User user, IEventHandler event_handler)
        {
            m_user = user;
            m_event_handler = event_handler;
            m_state = State.Stopped;
        }

        /// <summary>
        /// Start the consumer.
        /// </summary>
        /// <param name="auto_reconnect">True to have the consumer automatically reconnect if the connection is dropped.</param>
        public void consume(bool auto_reconnect = true)
        {
            m_state = State.Starting;
            onStart(auto_reconnect);
        }

        /// <summary>
        /// Stop the consumer. This sets a variable which is monitored by the
        /// consumer imlementation so there may be a delay between calling
        /// this method and the consumer actually stopping.
        /// </summary>
        public void stop()
        {
            if (m_state != State.Running)
            {
                throw new InvalidDataException("Consumer state must be RUNNING before it can be stopped");
            }
            m_state = State.Stopping;
        }

        /// <summary>
        /// Get the URL for the HTTP endpoint for this consumer.
        /// </summary>
        /// <returns>The URL.</returns>
        protected string getUrl()
        {
            if (m_hashes.Count == 0)
            {
                throw new InvalidDataException("At least one hash is required");
            }
            else if (m_hashes.Count == 1)
            {
                // Single stream.
                return "http://" + User.STREAM_BASE_URL + m_hashes[0];
            }
            else
            {
                // Multistram.
                return "http://" + User.STREAM_BASE_URL + "multi?hashes=" + String.Join(",", m_hashes.ToArray());
            }
        }

        /// <summary>
        /// Get the authentication header for the request.
        /// </summary>
        /// <returns>The value to be sent in the auth header.</returns>
        protected string getAuthHeader()
        {
            return m_user.getUsername() + ":" + m_user.getApiKey();
        }

        /// <summary>
        /// Get the user agent that should be given to the server.
        /// </summary>
        /// <returns>The user agent string.</returns>
        protected string getUserAgent()
        {
            return User.USER_AGENT;
        }

        /// <summary>
        /// Get the current state of this consumer.
        /// </summary>
        /// <returns>The current state.</returns>
        public State getState()
        {
            return m_state;
        }

        /// <summary>
        /// Check to see if this consumer is running. Send true in the first
        /// parameter to include STARTING as an indication that the consumer
        /// is running.
        /// </summary>
        /// <param name="allowStarting">Should STARTING be accepted as a running state.</param>
        /// <returns>True if this consumer is running.</returns>
        public bool isRunning(bool allowStarting = false)
        {
            return (allowStarting && m_state == State.Starting) || m_state == State.Running;
        }

        /// <summary>
        /// Called by derived classes when the consumer has successfully connected.
        /// </summary>
        protected void onConnect()
        {
            m_state = State.Running;
            m_event_handler.onConnect(this);
        }

        /// <summary>
        /// Called by derived classes when a complete JSON string has been received.
        /// </summary>
        /// <param name="json">The JSON string.</param>
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
                // The data contains a hash so the connection is consuming
                // multiple streams.
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
                // The data does not contain a hash so the connection is
                // consuming a single stream. Use the first element in
                // the hashes array as the hash for the callback.
                if (data.has("deleted"))
                {
                    m_event_handler.onDeleted(this, new Interaction(data.getJVal()), m_hashes[0]);
                }
                else
                {
                    m_event_handler.onInteraction(this, new Interaction(data.getJVal()), m_hashes[0]);
                }
            }
            else
            {
                // Hitting this means the data structure did not have any of
                // the expected elements.
                onError("Unhandled data received: " + json);
            }
        }

        /// <summary>
        /// Called by derived classes when a warning occurs.
        /// </summary>
        /// <param name="message">The warning message.</param>
        protected void onWarning(string message)
        {
            m_event_handler.onWarning(this, message);
        }

        /// <summary>
        /// Called by derived classes when an error occurs.
        /// </summary>
        /// <param name="message">The error message.</param>
        protected void onError(string message)
        {
            m_event_handler.onError(this, message);
        }

        /// <summary>
        /// Called by derived classes when the connection is disconnected.
        /// </summary>
        protected void onDisconnect()
        {
            m_event_handler.onDisconnect(this);
        }

        /// <summary>
        /// The method that must be implemented by derived classes to kick off
        /// the consumer.
        /// </summary>
        /// <param name="auto_reconnect">Whether to reconnect should the connection be dropped.</param>
        protected abstract void onStart(bool auto_reconnect = true);
    }
}
