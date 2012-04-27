using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    public class ApiResponse
    {
        private int m_response_code = -1;
        public int response_code
        {
            get
            {
                return m_response_code;
            }
        }

        private int m_rate_limit = -1;
        public int rate_limit
        {
            get
            {
                return m_rate_limit;
            }
        }

        private int m_rate_limit_remaining = -1;
        public int rate_limit_remaining
        {
            get
            {
                return m_rate_limit_remaining;
            }
        }

        private JSONdn m_data = null;
        public JSONdn data
        {
            get
            {
                return m_data;
            }
        }

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
