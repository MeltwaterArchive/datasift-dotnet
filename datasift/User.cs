using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    /// <summary>
    /// The User class encapsulates a DataSift user and provides access to all
    /// of the API functionality.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The user-agent string to be used when connecting to HTTP resources.
        /// </summary>
        public static string USER_AGENT
        {
            get
            {
                return "DataSiftNET/" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// The base URL for API requests.
        /// </summary>
        public static string API_BASE_URL
        {
            get
            {
                return "api.datasift.com/";
            }
        }

        /// <summary>
        /// The base URL for streams.
        /// </summary>
        public static string STREAM_BASE_URL
        {
            get
            {
                return "stream.datasift.com/";
            }
        }

        /// <summary>
        /// The User's username.
        /// </summary>
        private string m_username = string.Empty;

        /// <summary>
        /// Get the User's username.
        /// </summary>
        /// <returns>The username.</returns>
        public string getUsername()
        {
            return m_username;
        }

        /// <summary>
        /// The User's API key.
        /// </summary>
        private string m_api_key = string.Empty;

        /// <summary>
        /// Get the User's API key.
        /// </summary>
        /// <returns>The API key.</returns>
        public string getApiKey()
        {
            return m_api_key;
        }

        /// <summary>
        /// The User's current rate limit.
        /// </summary>
        private int m_rate_limit = -1;
        
        /// <summary>
        /// Get the User's current rate limit.
        /// </summary>
        /// <returns>The rate limit.</returns>
        public int getRateLimit()
        {
            return m_rate_limit;
        }

        /// <summary>
        /// The User's current remaining rate limit.
        /// </summary>
        private int m_rate_limit_remaining = -1;

        /// <summary>
        /// Get the User's current remaining rate limit.
        /// </summary>
        /// <returns></returns>
        public int getRateLimitRemaining()
        {
            return m_rate_limit_remaining;
        }

        /// <summary>
        /// The delegate used when calling the API.
        /// </summary>
        /// <param name="username">The username of the user making the API call.</param>
        /// <param name="api_key">The API key of the user making the API call.</param>
        /// <param name="endpoint">The endpoint to be called.</param>
        /// <param name="parameters">The parameters to be passed as POST data.</param>
        /// <returns>An ApiResponse object.</returns>
        public delegate ApiResponse CallAPIDelegate(string username, string api_key, string endpoint, Dictionary<string, string> parameters);
        
        /// <summary>
        /// The delegate to be used when making an API call. This enables the
        /// ApiClient object to be replaced. Leave it as null to use the
        /// default ApiClient class.
        /// </summary>
        CallAPIDelegate m_callapi = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="username">The User's username.</param>
        /// <param name="api_key">The User's API key.</param>
        public User(String username, String api_key)
        {
            m_username = username;
            m_api_key = api_key;
        }

        /// <summary>
        /// Set the API client delegate.
        /// </summary>
        /// <param name="callapi">The delegate.</param>
        public void setApiClient(CallAPIDelegate callapi)
        {
            m_callapi = callapi;
        }

        /// <summary>
        /// Get the usage for the given period.
        /// </summary>
        /// <param name="period">"hour" or "day"</param>
        /// <returns>A Usage object.</returns>
        public Usage getUsage(string period = "hour")
        {
            if (period != "hour" && period != "day")
            {
                throw new InvalidDataException("The period parameter must be a valid period.");
            }
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("period", period);
            return new Usage(callApi("usage", parameters));
        }

        /// <summary>
        /// Create a definition object for this user.
        /// </summary>
        /// <param name="csdl">Optional initial CSDL for the definition.</param>
        /// <returns></returns>
        public Definition createDefinition(string csdl = "")
        {
            return new Definition(this, csdl);
        }

        /// <summary>
        /// Get a StreamConsumer object for the given stream hash.
        /// </summary>
        /// <param name="hash">The stream hash.</param>
        /// <param name="event_handler">The object that will receive events.</param>
        /// <param name="type">The type of StreamConsumer required.</param>
        /// <returns>A StreamConsumer object.</returns>
        public StreamConsumer getConsumer(string hash, IEventHandler event_handler, string type = "http")
        {
            return StreamConsumer.factory(this, type, new Definition(this, null, hash), event_handler);
        }

        /// <summary>
        /// Get a StreamConsumer object for the given array of stream hashes.
        /// </summary>
        /// <param name="hashes">The array of stream hashes.</param>
        /// <param name="event_handler">The object that will receive events.</param>
        /// <param name="type">The type of StreamConsumer required.</param>
        /// <returns>A StreamConsumer object.</returns>
        public StreamConsumer getMultiConsumer(string[] hashes, IEventHandler event_handler, string type = "http")
        {
            return StreamConsumer.factory(this, type, hashes, event_handler);
        }

        /// <summary>
        /// Call the DataSift API.
        /// </summary>
        /// <param name="endpoint">The endpoint to call.</param>
        /// <param name="parameters">The parameters to be passed.</param>
        /// <returns>A JSONdn object containing the JSON response data.</returns>
        public JSONdn callApi(string endpoint, Dictionary<string, string> parameters)
        {
            string errmsg = "";

            if (m_callapi == null)
            {
                m_callapi = new CallAPIDelegate(ApiClient.callAPI);
            }

            ApiResponse res = m_callapi(m_username, m_api_key, endpoint, parameters);

            m_rate_limit = res.rate_limit;
            m_rate_limit_remaining = res.rate_limit_remaining;

            switch (res.response_code)
            {
                case 200:
                    return res.data;
                case 401:
                    errmsg = "Authentication failed";
                    if (res.data.has("error"))
                    {
                        errmsg = res.data.getStringVal("error");
                    }
                    throw new AccessDeniedException(errmsg);
                case 403:
                    if (m_rate_limit_remaining == 0)
                    {
                        throw new RateLimitExceededException(/* get 'comment' from JSON */);
                    }
                    goto default;
                default:
                    errmsg = "Unknown error (" + res.response_code + ")";
                    if (res.data.has("error"))
                    {
                        errmsg = res.data.getStringVal("error");
                    }
                    throw new ApiException(errmsg, res.response_code);
            }
        }
    }
}
