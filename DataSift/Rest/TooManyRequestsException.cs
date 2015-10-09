using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest
{
    public class TooManyRequestsException : RestAPIException
    {
        public TooManyRequestsException(RestAPIResponse response, string message)
            : base(response, message) {}
    }
}
