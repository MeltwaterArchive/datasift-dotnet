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
        /// The delegate to be used when making an API call. This enables the
        /// ApiClient object to be replaced. Leave it as null to use the
        /// default ApiClient class.
        /// </summary>
        AbstractApiClient m_apiclient = null;

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
        public void setApiClient(AbstractApiClient apiclient)
        {
            m_apiclient = apiclient;
        }

        /// <summary>
        /// Get the usage for the given period.
        /// </summary>
        /// <param name="period">"hour" or "day"</param>
        /// <returns>A Usage object.</returns>
        public Usage getUsage(string period = "hour")
        {
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
        /// Create a new Historics query.
        /// </summary>
        /// <param name="hash">The stream hash that this Historic will use.</param>
        /// <param name="start_date">The start date for the query.</param>
        /// <param name="end_date">The end date for the query.</param>
        /// <param name="sources">An array of data sources for the query.</param>
        /// <param name="sample">The sample rate for the query.</param>
        /// <param name="name">A friendly name for the query.</param>
        public Historic createHistoric(string hash, DateTime start_date, DateTime end_date, List<string> sources, double sample, string name)
        {
            return new Historic(this, hash, start_date, end_date, sources, sample, name);
        }

        /// <summary>
        /// Get a list of Historics queries in your account.
        /// </summary>
        /// <param name="page">The page number to get.</param>
        /// <param name="per_page">The number of items per page.</param>
        /// <returns>A list of Historic objects.</returns>
        public List<Historic> listHistorics(int page = 1, int per_page = 20)
        {
            return Historic.list(this, page, per_page);
        }

        /// <summary>
        /// Create a new PushDefinition object belonging to this user.
        /// </summary>
        /// <returns>An empty PushDefinition object.</returns>
        public PushDefinition createPushDefinition()
        {
            return new PushDefinition(this);
        }

        /// <summary>
        /// Get a page of Push subscriptions for the given stream hash in the
        /// user's account, where each page contains up to per_page
        /// items. Results will be ordered according to the supplied ordering
        /// parameters.
        /// </summary>
        /// <param name="hash">The stream hash.</param>
        /// <param name="page">The page number to get.</param>
        /// <param name="per_page">The number of items per page.</param>
        /// <param name="order_by">The field by which to order the results.</param>
        /// <param name="order_dir">Ascending or descending.</param>
        /// <returns>A PushSubscriptionList object.</returns>
        public PushSubscriptionList listPushSubscriptionsByStreamHash(string hash, int page, int per_page, string order_by = PushSubscription.ORDERBY_CREATED_AT, string order_dir = PushSubscription.ORDERDIR_ASC)
        {
            return PushSubscription.listByStreamHash(this, hash, page, per_page, order_by, order_dir);
        }

        /// <summary>
        /// Get a page of Push subscriptions for the given Historics query
        /// playback ID in the given user's account, where each page contains
        /// up to per_page items. Results will be ordered according to the
        /// supplied ordering parameters.
        /// </summary>
        /// <param name="playback_id">The playback ID.</param>
        /// <param name="page">The page number to get.</param>
        /// <param name="per_page">The number of items per page.</param>
        /// <param name="order_by">The field by which to order the results.</param>
        /// <param name="order_dir">Ascending or descending.</param>
        /// <param name="include_finished">True to include subscriptions against finished Historics queries.</param>
        /// <returns>A PushSubscriptionList object.</returns>
        public PushSubscriptionList listPushSubscriptionsByPlaybackId(string playback_id, int page, int per_page, string order_by = PushSubscription.ORDERBY_CREATED_AT, string order_dir = PushSubscription.ORDERDIR_ASC, bool include_finished = false)
        {
            return PushSubscription.listByPlaybackId(this, playback_id, page, per_page, order_by, order_dir, include_finished);
        }
        
        /// <summary>
        /// Get a page of Push subscriptions the user's account,
        /// where each page contains up to per_page items. Results will be
        /// ordered according to the supplied ordering parameters.
        /// </summary>
        /// <param name="page">The page number to get.</param>
        /// <param name="per_page">The number of items per page.</param>
        /// <param name="order_by">The field by which to order the results.</param>
        /// <param name="order_dir">Ascending or descending.</param>
        /// <param name="include_finished">True to include subscriptions against finished Historics queries.</param>
        /// <param name="hash_type">Optional hash type to look for (hash is also required)</param>
        /// <param name="hash">Optional hash to look for (hash_type is also required)</param>
        /// <returns>A PushSubscriptionList object.</returns>
        public PushSubscriptionList listPushSubscriptions(int page, int per_page, string order_by = PushSubscription.ORDERBY_CREATED_AT, string order_dir = PushSubscription.ORDERDIR_ASC, bool include_finished = false)
        {
            return PushSubscription.list(this, page, per_page, order_by, order_dir, include_finished);
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
            string errmsg = String.Empty;

            if (m_apiclient == null)
            {
                m_apiclient = new ApiClient(getUsername(), getApiKey(), API_BASE_URL, USER_AGENT);
            }

            ApiResponse res = m_apiclient.callAPI(endpoint, parameters);

            m_rate_limit = res.rate_limit;
            m_rate_limit_remaining = res.rate_limit_remaining;

            switch (res.response_code)
            {
                case 200:
                case 202:
                case 204:
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
