using RestSharp;
using RestSharp.Contrib;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest.Pylon
{
    public class Reference
    {
        DataSiftClient _client = null;

        internal Reference(DataSiftClient client)
        {
            _client = client;
        }

        public RestAPIResponse Get(string service, string slug = null, int? page = null, int? perPage = null)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            Contract.Requires<ArgumentException>((slug != null) ? slug.Trim().Length > 0 : true);

            Contract.Requires<ArgumentException>((page.HasValue) ? page.Value > 0 : true);
            Contract.Requires<ArgumentException>((perPage.HasValue) ? perPage.Value > 0 : true);

            if (slug != null)
                return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/reference/" + HttpUtility.UrlEncode(slug), null, Method.GET);
            else
                return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/reference", new { page = page, per_page = perPage }, Method.GET);
            
        }
    }
}
