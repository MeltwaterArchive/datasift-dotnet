using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    /// <summary>
    /// The mock version of the ApiClient which is used in the unit tests.
    /// </summary>
    public class MockApiClient : AbstractApiClient
    {
        /// <summary>
        /// The ApiResponse object that will currently be returned by calls to
        /// callAPI.
        /// </summary>
        static private ApiResponse m_response = null;

        /// <summary>
        /// Set the ApiResponse object to be returned by calls to callAPI.
        /// </summary>
        /// <param name="response">The ApiResponse object.</param>
        static public void setAPIResponse(ApiResponse response)
        {
            m_response = response;
        }

        /// <summary>
        /// Construct a new instance of this object with the user's username and API key. 
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="api_key">The user's API key.</param>
        /// <param name="base_url">The base URL for API requests.</param>
        /// <param name="user_agent">The user agent to send with all requests.s</param>
        public MockApiClient(string username, string api_key, string base_url = null, string user_agent = null)
            : base(username, api_key, base_url, user_agent)
        {
        }

        /// <summary>
        /// Get the mock response object.
        /// </summary>
        /// <param name="endpoint">ignored</param>
        /// <param name="parameters">ignored</param>
        /// <returns>An ApiResponse object.</returns>
        override public ApiResponse callAPI(string endpoint, Dictionary<string, string> parameters = null)
        {
            return m_response;
        }
    }
}
