using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest
{
    public class RestAPIException : Exception
    {
        public RestAPIResponse Response { get; set; }

        public RestAPIException(RestAPIResponse response, string message) : base(message)
        {
            this.Response = response;
        }
    }
}
