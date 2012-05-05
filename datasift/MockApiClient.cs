using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    /// <summary>
    /// The mock version of the ApiClient which is used in the unit tests.
    /// </summary>
    public class MockApiClient
    {
        /// <summary>
        /// The ApiResponse object that will currently be returned by calls to
        /// callAPI.
        /// </summary>
        private static ApiResponse m_response = null;

        /// <summary>
        /// Get the mock response object.
        /// </summary>
        /// <param name="username">ignored</param>
        /// <param name="api_key">ignored</param>
        /// <param name="endpoint">ignored</param>
        /// <param name="parameters">ignored</param>
        /// <returns>An ApiResponse object.</returns>
        public static ApiResponse callAPI(string username, string api_key, string endpoint, Dictionary<string, string> parameters)
        {
            return m_response;
        }

        /// <summary>
        /// Set the ApiResponse object to be returned by calls to callAPI.
        /// </summary>
        /// <param name="response">The ApiResponse object.</param>
        public static void setAPIResponse(ApiResponse response)
        {
            m_response = response;
        }
    }
}
