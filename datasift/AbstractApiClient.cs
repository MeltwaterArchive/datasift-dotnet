using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace datasift
{
    public class AbstractApiClient
    {
        /// <summary>
        /// The user's username.
        /// </summary>
        protected string m_username = String.Empty;

        /// <summary>
        /// The user's API key.
        /// </summary>
        protected string m_api_key = String.Empty;

        /// <summary>
        /// The base URL to use for API calls, without the http:// but including the trailing /.
        /// </summary>
        protected string m_base_url = String.Empty;

        /// <summary>
        /// The user agent to send with all requests.
        /// </summary>
        protected string m_user_agent = String.Empty;

        /// <summary>
        /// Parameters.
        /// </summary>
        protected Dictionary<string, string> m_parameters = new Dictionary<string, string>();

        /// <summary>
        /// .NET appears to require a constructor that takes no arguments.
        /// </summary>
        protected AbstractApiClient()
        {
            // For some reason this is required!
        }

        /// <summary>
        /// Construct a new instance of this object with the user's username and API key. 
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="api_key">The user's API key.</param>
        /// <param name="base_url">The base URL for API requests.</param>
        /// <param name="user_agent">The user agent to send with all requests.s</param>
        public AbstractApiClient(string username, string api_key, string base_url = null, string user_agent = null)
        {
            m_username = username;
            m_api_key = api_key;
            m_base_url = (base_url == null ? User.API_BASE_URL : base_url);
            m_user_agent = (user_agent == null ? User.USER_AGENT : user_agent);
        }

        /// <summary>
        /// Set a parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        public void setParameter(string name, string value)
        {
            m_parameters.Add(name, value);
        }

        /// <summary>
        /// Remove a parameter.
        /// </summary>
        /// <param name="name">The name of the parameter to remove.</param>
        public void removeParameter(string name)
        {
            m_parameters.Remove(name);
        }

        /// <summary>
        /// Reset the state of the object.
        /// </summary>
        public void reset()
        {
            m_parameters.Clear();
        }

        /// <summary>
        /// Build a form-encoded (i.e. query string) from a set of parameters.
        /// </summary>
        /// <param name="parameters">An optional Dictionary of parameters, or omit to use the object's parameters.</param>
        /// <returns>A string.</returns>
        public string getPostData(Dictionary<string, string> parameters = null)
        {
            StringBuilder post_data = new StringBuilder();
            var is_first = true;
            foreach (KeyValuePair<string, string> kv in (parameters == null ? m_parameters : parameters))
            {
                post_data.AppendFormat(is_first?"{0}={1}":"&{0}={1}", HttpUtility.UrlEncode(kv.Key), HttpUtility.UrlEncode(kv.Value));
                is_first = false;
            }
            return post_data.ToString();
        }

        /// <summary>
        /// Make an API call. Implementations must override this!
        /// </summary>
        /// <param name="endpoint">The endpoint to call.</param>
        /// <param name="parameters">Optional parameters. If omitted the parameters set on the object will be used.</param>
        /// <returns>An ApiResponse object.</returns>
        virtual public ApiResponse callAPI(string endpoint, Dictionary<string, string> parameters = null)
        {
            throw new NotImplementedException();
        }
    }
}
