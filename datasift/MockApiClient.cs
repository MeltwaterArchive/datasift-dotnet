using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    public class MockApiClient
    {
        private static ApiResponse m_response = null;

        public static ApiResponse callAPI(string username, string api_key, string endpoint, Dictionary<string, string> parameters)
        {
            return m_response;
        }

        public static void setAPIResponse(ApiResponse response)
        {
            m_response = response;
        }
    }
}
