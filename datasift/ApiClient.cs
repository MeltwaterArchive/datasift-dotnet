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
    class ApiClient
    {
        /// <summary>
        /// Calls an endpoint on the DataSift HTTP API.
        /// </summary>
        /// <param name="username">Your DataSift username.</param>
        /// <param name="api_key">Your DataSift API key.</param>
        /// <param name="endpoint">The endpoint to call.</param>
        /// <param name="parameters">The parameters to pass as POST data.</param>
        /// <returns>An ApiResponse instance containing the response details.</returns>
        public static ApiResponse callAPI(string username, string api_key, string endpoint, Dictionary<string, string> parameters)
        {
            // Set up the request
            string url = "http://" + User.API_BASE_URL + endpoint + ".json";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Headers["Auth"] = username + ":" + api_key;
            req.UserAgent = User.USER_AGENT;
            req.Method = "POST";

            // Add the POST data
            StringBuilder post_data = new StringBuilder();
            foreach (KeyValuePair<string,string> kv in parameters)
            {
                post_data.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(kv.Key), HttpUtility.UrlEncode(kv.Value));
            }
            byte[] post_data_bytes = Encoding.UTF8.GetBytes(post_data.Remove(post_data.Length - 1, 1).ToString());
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
