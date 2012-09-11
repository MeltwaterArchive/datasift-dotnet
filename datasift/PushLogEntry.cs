using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    public class PushLogEntry
    {
        /// <summary>
        /// The subscription ID.
        /// </summary>
        protected string m_subscription_id = "";

        /// <summary>
        /// The request time.
        /// </summary>
        protected DateTime m_request_time = DateTime.MinValue;

        /// <summary>
        /// Whether this log entry represents a successful operation.
        /// </summary>
        protected bool m_success = false;

        /// <summary>
        /// The log message.
        /// </summary>
        protected string m_message = "";

        /// <summary>
        /// Constructor from a JSON object.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        public PushLogEntry(JSONdn json)
            : this(json.getStringVal("subscription_id"), Utils.UnixTimestampToDateTime(json.getLongVal("request_time")), json.getBoolVal("success"), json.has("message") ? json.getStringVal("message") : "")
        {
        }

        /// <summary>
        /// Constructor from individual paramters.
        /// </summary>
        /// <param name="subscription_id">The subscription ID.</param>
        /// <param name="request_time">The request time.</param>
        /// <param name="success">Whether this entry represents a successful operation.</param>
        /// <param name="message">The log message.</param>
        public PushLogEntry(string subscription_id, DateTime request_time, bool success, string message)
        {
            m_subscription_id = subscription_id;
            m_request_time = request_time;
            m_success = success;
            m_message = message;
        }

        /// <summary>
        /// Accessor for the subscription ID.
        /// </summary>
        public string SubscriptionId { get { return m_subscription_id; } }

        /// <summary>
        /// Accessor for the request time.
        /// </summary>
        public DateTime RequestTime { get { return m_request_time; } }

        /// <summary>
        /// Accessor for whether this entry represents a successful operation.
        /// </summary>
        public bool Success { get { return m_success; } }

        /// <summary>
        /// Accessor for the log message.
        /// </summary>
        public string Message { get { return m_message; } }
    }
}
