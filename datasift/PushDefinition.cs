using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    /// <summary>
    /// Represents the configuration of a Push endpoint.
    /// </summary>
    public class PushDefinition
    {
        /// <summary>
        /// The prefix to be used when passing the output_params to API calls.
        /// </summary>
        public const string OUTPUT_PARAMS_PREFIX = "output_params.";

        /// <summary>
        /// The user that owns this Push definition.
        /// </summary>
        protected User m_user = null;

        /// <summary>
        /// An initial status for Push subscriptions.
        /// </summary>
        protected string m_initial_status = "";

        /// <summary>
        /// The output type of this Push definition.
        /// </summary>
        protected string m_output_type = "";

        /// <summary>
        /// The output parameters.
        /// </summary>
        protected PushOutputParams m_output_params = new PushOutputParams();

        /// <summary>
        /// Constructor. Takes the user creating the object.
        /// </summary>
        /// <param name="user">The user creating the object.</param>
        public PushDefinition(User user)
        {
            m_user = user;
        }

        /// <summary>
        /// Gets the initial status for subscriptions.
        /// </summary>
        /// <returns>The initial status.</returns>
        public string getInitialStatus()
        {
            return m_initial_status;
        }

        /// <summary>
        /// Sets the initial status for subscriptions.
        /// </summary>
        /// <param name="status">The new initial status for subscriptions.</param>
        public void setInitialStatus(string status)
        {
            m_initial_status = status;
        }

        /// <summary>
        /// Get the output type.
        /// </summary>
        /// <returns>The output type.</returns>
        public string getOutputType()
        {
            return m_output_type;
        }

        /// <summary>
        /// Sets the output type.
        /// </summary>
        /// <param name="output_type">The new output type.</param>
        public void setOutputType(string output_type)
        {
            m_output_type = output_type;
        }

        /// <summary>
        /// Sets an output parameter.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="val">The new parameter value.</param>
        public void setOutputParam(string key, string val)
        {
            m_output_params.set(key, val);
        }

        /// <summary>
        /// Gets an output parameters.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <returns>The parameter value, or null if not set.</returns>
        public string getOutputParam(string key)
        {
            return m_output_params[key];
        }

        /// <summary>
        /// Get all of the output parameters.
        /// </summary>
        /// <returns>A PushOutputParams object.</returns>
        public PushOutputParams getOutputParams()
        {
            return m_output_params;
        }

        /// <summary>
        /// Validate the output type and parameters.
        /// </summary>
        public void validate()
        {
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("output_type", m_output_type);
                foreach (string key in m_output_params.Keys)
                {
                    parameters.Add(OUTPUT_PARAMS_PREFIX + key, m_output_params[key]);
                }
                m_user.callApi("push/validate", parameters);
            }
            catch (ApiException e)
            {
                throw new InvalidDataException(e.Message);
            }
        }

        /// <summary>
        /// Subscribe this endpoint to a Definition.
        /// </summary>
        /// <param name="definition">The Definition to which to subscribe.</param>
        /// <param name="name">A name for this subscription.</param>
        /// <returns>A PushSubscription object.</returns>
        public PushSubscription subscribe(Definition definition, string name)
        {
            return subscribeStreamHash(definition.getHash(), name);
        }

        /// <summary>
        /// Subscribe this endpoint to a stream hash.
        /// </summary>
        /// <param name="hash">The stream hash.</param>
        /// <param name="name">A name for this subscription.</param>
        /// <returns>A PushSubscription object.</returns>
        public PushSubscription subscribeStreamHash(string hash, string name)
        {
            return subscribe("hash", hash, name);
        }

        /// <summary>
        /// Subscribe this endpoint to a Historics query.
        /// </summary>
        /// <param name="historic">The Historics query.</param>
        /// <param name="name">A name for this subscription.</param>
        /// <returns>A PushSubscription object.</returns>
        public PushSubscription subscribe(Historic historic, string name)
        {
            return subscribeHistoricPlaybackId(historic.getHash(), name);
        }

        /// <summary>
        /// Subscribe this endpoint to a Historics query playback ID.
        /// </summary>
        /// <param name="playback_id">The playback ID.</param>
        /// <param name="name">A name for this subscription.</param>
        /// <returns>A PushSubscription object.</returns>
        public PushSubscription subscribeHistoricPlaybackId(string playback_id, string name)
        {
            return subscribe("playback_id", playback_id, name);
        }

        /// <summary>
        /// Subscribe this endpoint to a stream hash or Historics query
        /// playback ID.
        /// </summary>
        /// <param name="hash_type">"hash" or "playback_id"</param>
        /// <param name="hash">The hash or playback ID.</param>
        /// <param name="name">A name for this subscription.</param>
        /// <returns>A PushSubscription object.</returns>
        public PushSubscription subscribe(string hash_type, string hash, string name)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("name", name);
            parameters.Add(hash_type, hash);
            parameters.Add("output_type", m_output_type);
            foreach (string key in m_output_params.Keys)
            {
                parameters.Add(OUTPUT_PARAMS_PREFIX + key, m_output_params[key]);
            }

            // Add the initial status if it's not empty
            if (m_initial_status.Length > 0)
            {
                parameters.Add("initial_status", m_initial_status);
            }

            return new PushSubscription(m_user, m_user.callApi("push/create", parameters));
        }
    }
}
