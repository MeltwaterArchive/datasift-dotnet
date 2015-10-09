using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest
{
    public interface IIngestAPIRequest
    {
        RestAPIResponse Ingest(string endpoint, dynamic data, Method method = Method.POST);
    }
}
