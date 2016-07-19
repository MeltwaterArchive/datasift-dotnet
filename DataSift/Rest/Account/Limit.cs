using RestSharp;
using RestSharp.Contrib;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest.Account
{
    public class Limit
    {
        DataSiftClient _client = null;

        internal Limit(DataSiftClient client)
        {
            _client = client;
        }

        public RestAPIResponse Create(string identityId, string service, int? totalAllowance = null, int? analyzeQueries = null)
        {
            Contract.Requires<ArgumentNullException>(identityId != null);
            Contract.Requires<ArgumentException>((identityId != null) ? identityId.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((identityId != null) ? Constants.IDENTITY_ID_FORMAT.IsMatch(identityId) : true, Messages.INVALID_IDENTITY_ID);
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((totalAllowance != null) ? totalAllowance > 0 : true);
            Contract.Requires<ArgumentException>((analyzeQueries != null) ? analyzeQueries > 0 : true);

            return _client.GetRequest().Request("account/identity/" + identityId + "/limit", new { service = service, total_allowance = totalAllowance, analyze_queries = analyzeQueries }, Method.POST);
        }

        public RestAPIResponse Get(string service, string identityId = null, int? page = null, int? perPage = null)
        {
            Contract.Requires<ArgumentException>((identityId != null) ? identityId.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((identityId != null) ? Constants.IDENTITY_ID_FORMAT.IsMatch(identityId) : true, Messages.INVALID_IDENTITY_ID);
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((page.HasValue) ? page.Value > 0 : true);
            Contract.Requires<ArgumentException>((perPage.HasValue) ? perPage.Value > 0 : true);

            if (identityId != null)
                return _client.GetRequest().Request("account/identity/" + identityId + "/limit/" + HttpUtility.UrlEncode(service), null, Method.GET);
            else
                return _client.GetRequest().Request("account/identity/limit/" + HttpUtility.UrlEncode(service), new { page = page, per_page = perPage }, Method.GET);
        }

        public RestAPIResponse Update(string identityId, string service, int? totalAllowance = null, int? analyzeQueries = null)
        {
            Contract.Requires<ArgumentNullException>(identityId != null);
            Contract.Requires<ArgumentException>((identityId != null) ? identityId.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((identityId != null) ? Constants.IDENTITY_ID_FORMAT.IsMatch(identityId) : true, Messages.INVALID_IDENTITY_ID);
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((totalAllowance != null) ? totalAllowance > 0 : true);
            Contract.Requires<ArgumentException>((analyzeQueries != null) ? analyzeQueries > 0 : true);

            return _client.GetRequest().Request("account/identity/" + identityId + "/limit/" + HttpUtility.UrlEncode(service), new { total_allowance = totalAllowance, analyze_queries = analyzeQueries }, Method.PUT);
        }

        public RestAPIResponse Delete(string identityId, string service)
        {
            Contract.Requires<ArgumentNullException>(identityId != null);
            Contract.Requires<ArgumentException>((identityId != null) ? identityId.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((identityId != null) ? Constants.IDENTITY_ID_FORMAT.IsMatch(identityId) : true, Messages.INVALID_IDENTITY_ID);
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            return _client.GetRequest().Request("account/identity/" + identityId + "/limit/" + HttpUtility.UrlEncode(service), null, Method.DELETE);
        }
    }
}
