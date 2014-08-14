using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace DataSift.Rest
{
    public interface IRestAPIRequest
    {
        RestAPIResponse Request(string endpoint, dynamic parameters = null, Method method = Method.GET);
    }
}
