using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

/*
 * A User instance represents a DataSift user and provides access to all of
 * the API functionality.
 */

namespace datasift
{
    public class User
    {
        // The user-agent string to use when connecting to remote resources.
        public static string USER_AGENT
        {
            get
            {
                return "DataSiftNET/" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string API_BASE_URL
        {
            get
            {
                return "api.datasift.com/";
            }
        }

        public static string STREAM_BASE_URL
        {
            get
            {
                return "stream.datasift.com/";
            }
        }

        private string m_username = string.Empty;
        public string getUsername()
        {
            return m_username;
        }

        private string m_api_key = string.Empty;
        public string getApiKey()
        {
            return m_api_key;
        }

        private int m_rate_limit = -1;
        public int getRateLimit()
        {
            return m_rate_limit;
        }

        private int m_rate_limit_remaining = -1;
        public int getRateLimitRemaining()
        {
            return m_rate_limit_remaining;
        }

        public delegate ApiResponse CallAPIDelegate(string username, string api_key, string endpoint, Dictionary<string, string> parameters);
        CallAPIDelegate m_callapi = null;

        public User(String username, String api_key)
        {
            m_username = username;
            m_api_key = api_key;
        }

        public void setApiClient(CallAPIDelegate callapi)
        {
            m_callapi = callapi;
        }

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

        /**
         * Create a definition object for this user. If a CSDL parameter is
         * provided then this will be used as the initial CSDL for the
         * definition.
         */
        public Definition createDefinition(string csdl = "")
        {
            return new Definition(this, csdl);
        }

        public StreamConsumer getConsumer(string hash, IEventHandler event_handler, string type = "http")
        {
            return StreamConsumer.factory(this, type, new Definition(this, null, hash), event_handler);
        }

        public StreamConsumer getMultiConsumer(string[] hashes, IEventHandler event_handler, string type = "http")
        {
            return StreamConsumer.factory(this, type, hashes, event_handler);
        }

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
