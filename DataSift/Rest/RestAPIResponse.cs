using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest
{
    public class RestAPIResponse
    {
        public dynamic Data { get; set; }
        public RateLimitInfo RateLimit { get; set; }

        public System.Net.HttpStatusCode StatusCode { get; set; }

        public string ToJson()
        {
            if (Data != null)
                return JsonConvert.SerializeObject(Data);
            else
                return null;
        }
    }

    public class RateLimitInfo
    {
        public int Limit { get; set; }
        public int Remaining { get; set; }
        public int Cost { get; set; }
        public long Reset { get; set; }
        public int ResetTtl { get; set; }
    }
}
