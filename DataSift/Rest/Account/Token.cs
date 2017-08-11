using RestSharp;
using System.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest.Account
{
    public class Token
    {
        DataSiftClient _client = null;

        internal Token(DataSiftClient client)
        {
            _client = client;
        }

        public RestAPIResponse Create(string identityId, string service, string token)
        {
            Contract.Requires<ArgumentNullException>(identityId != null);
            Contract.Requires<ArgumentException>((identityId != null) ? identityId.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((identityId != null) ? Constants.IDENTITY_ID_FORMAT.IsMatch(identityId) : true, Messages.INVALID_IDENTITY_ID);
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<ArgumentException>((token != null) ? token.Trim().Length > 0 : true);

            return _client.GetRequest().Request("account/identity/" + identityId + "/token", new { service = service, token = token }, Method.POST);
        }

        public RestAPIResponse Get(string identityId, string service = null, int? page = null, int? perPage = null)
        {
            Contract.Requires<ArgumentNullException>(identityId != null);
            Contract.Requires<ArgumentException>((identityId != null) ? identityId.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((identityId != null) ? Constants.IDENTITY_ID_FORMAT.IsMatch(identityId) : true, Messages.INVALID_IDENTITY_ID);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((page.HasValue) ? page.Value > 0 : true);
            Contract.Requires<ArgumentException>((perPage.HasValue) ? perPage.Value > 0 : true);

            if (service != null)
                return _client.GetRequest().Request("account/identity/" + identityId + "/token/" + HttpUtility.UrlEncode(service), null, Method.GET);
            else
                return _client.GetRequest().Request("account/identity/" + identityId + "/token", new { page = page, per_page = perPage }, Method.GET);
        }

        public RestAPIResponse Update(string identityId, string service, string token)
        {
            Contract.Requires<ArgumentNullException>(identityId != null);
            Contract.Requires<ArgumentException>((identityId != null) ? identityId.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((identityId != null) ? Constants.IDENTITY_ID_FORMAT.IsMatch(identityId) : true, Messages.INVALID_IDENTITY_ID);
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<ArgumentException>((token != null) ? token.Trim().Length > 0 : true);

            return _client.GetRequest().Request("account/identity/" + identityId + "/token/" + HttpUtility.UrlEncode(service), new { token = token }, Method.PUT);
        }

        public RestAPIResponse Delete(string identityId, string service)
        {
            Contract.Requires<ArgumentNullException>(identityId != null);
            Contract.Requires<ArgumentException>((identityId != null) ? identityId.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((identityId != null) ? Constants.IDENTITY_ID_FORMAT.IsMatch(identityId) : true, Messages.INVALID_IDENTITY_ID);
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            return _client.GetRequest().Request("account/identity/" + identityId + "/token/" + HttpUtility.UrlEncode(service), null, Method.DELETE);
        }
    }
}
