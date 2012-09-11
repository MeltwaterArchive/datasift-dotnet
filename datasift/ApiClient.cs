using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace datasift
{
    /// <summary>
    /// The ApiClient class is the default implementation for making HTTP
    /// requests to the DataSift API.
    /// </summary>
    public class ApiClient : AbstractApiClient
    {
        /// <summary>
        /// Construct a new instance of this object with the user's username and API key. 
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="api_key">The user's API key.</param>
        /// <param name="base_url">The base URL for API requests.</param>
        /// <param name="user_agent">The user agent to send with all requests.s</param>
        public ApiClient(string username, string api_key, string base_url = null, string user_agent = null)
            : base(username, api_key, base_url, user_agent)
        {
        }

        /// <summary>
        /// Calls an endpoint on the DataSift HTTP API.
        /// </summary>
        /// <param name="endpoint">The endpoint to call.</param>
        /// <param name="parameters">The parameters to pass as POST data.</param>
        /// <returns>An ApiResponse instance containing the response details.</returns>
        override public ApiResponse callAPI(string endpoint, Dictionary<string, string> parameters = null)
        {
            // Set up the request
            string url = "http://" + m_base_url + endpoint + ".json";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Headers["Auth"] = m_username + ":" + m_api_key;
            req.UserAgent = m_user_agent;
            req.Method = "POST";

            // Add the POST data
            byte[] post_data_bytes = Encoding.UTF8.GetBytes(getPostData(parameters));
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = post_data_bytes.Length;
            Stream post_stream = req.GetRequestStream();
            post_stream.Write(post_data_bytes, 0, post_data_bytes.Length);
            post_stream.Close();

            // Make the request
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException e)
            {
                res = (HttpWebResponse)e.Response;
            }

            int rate_limit = res.GetResponseHeader("x-ratelimit-limit").Length > 0 ? Convert.ToInt16(res.GetResponseHeader("x-ratelimit-limit")) : -1;
            int rate_limit_remaining = res.GetResponseHeader("x-ratelimit-limit").Length > 0 ? Convert.ToInt16(res.GetResponseHeader("x-ratelimit-remaining")) : -1;

            // Get the response and build the return value
            StreamReader response_stream = new StreamReader(res.GetResponseStream());
            ApiResponse retval = new ApiResponse((int)res.StatusCode, response_stream.ReadToEnd(), rate_limit, rate_limit_remaining);
            response_stream.Close();

            return retval;
        }
    }
}
