using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    /// <summary>
    /// This class represents a response from the API. Once created, objects
    /// are immutable.
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// The HTTP response code.
        /// </summary>
        private int m_response_code = -1;
        public int response_code
        {
            get
            {
                return m_response_code;
            }
        }

        /// <summary>
        /// The value of the x-rate-limit-limit header.
        /// </summary>
        private int m_rate_limit = -1;
        public int rate_limit
        {
            get
            {
                return m_rate_limit;
            }
        }

        /// <summary>
        /// The value of the x-rate-limit-remaining header.
        /// </summary>
        private int m_rate_limit_remaining = -1;
        public int rate_limit_remaining
        {
            get
            {
                return m_rate_limit_remaining;
            }
        }

        /// <summary>
        /// The data from the response as a JSONdn object.
        /// </summary>
        private JSONdn m_data = null;
        public JSONdn data
        {
            get
            {
                return m_data;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="status_code">The HTTP status code.</param>
        /// <param name="response_data">The content of the response - expected to be JSON.</param>
        /// <param name="rate_limit">The value of the x-rate-limit-limit header.</param>
        /// <param name="rate_limit_remaining">The value of the x-rate-limit-remaining header.</param>
        public ApiResponse(int status_code, string response_data, int rate_limit = -1, int rate_limit_remaining = -1)
        {
            try
            {
                m_response_code = status_code;
                m_data = new JSONdn(response_data);
                m_rate_limit = rate_limit;
                m_rate_limit_remaining = rate_limit_remaining;
            }
            catch (Exception e)
            {
                throw new ApiException("Request failed: " + e.Message, 503);
            }
        }
    }
}
