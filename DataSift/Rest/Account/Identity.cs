using DataSift.Enum;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest.Account
{
    public class Identity
    {
        DataSiftClient _client = null;

        private Token _token;
        private Limit _limit;

        internal Identity(DataSiftClient client)
        {
            _client = client;
        }

        public Token Token
        {
            get
            {
                if (_token == null) _token = new Token(_client);
                return _token;
            }
        }

        public Limit Limit
        {
            get
            {
                if (_limit == null) _limit = new Limit(_client);
                return _limit;
            }
        }

        public RestAPIResponse Create(string label,  IdentityStatus? status = null, bool? master = null)
        {
            Contract.Requires<ArgumentNullException>(label != null);
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(label));

            return _client.GetRequest().Request("account/identity", new { label = label, status = status, master = master }, Method.POST);
        }

        public RestAPIResponse Get(string id = null, string label = null, int? page = null, int? perPage = null)
        {
            Contract.Requires<ArgumentException>((id != null) ? id.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((id != null) ? Constants.IDENTITY_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_IDENTITY_ID);
            Contract.Requires<ArgumentException>((label != null) ? label.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((page.HasValue) ? page.Value > 0 : true);
            Contract.Requires<ArgumentException>((perPage.HasValue) ? perPage.Value > 0 : true);

            if(id != null)
                return _client.GetRequest().Request("account/identity/" + id, null, Method.GET);
            else
                return _client.GetRequest().Request("account/identity", new { id = id, label = label, page = page, per_page = perPage }, Method.GET);
        }

        public RestAPIResponse Update(string id, string label, IdentityStatus? status = null, bool? master = null)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>((id != null) ? id.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((id != null) ? Constants.IDENTITY_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_IDENTITY_ID);
            Contract.Requires<ArgumentException>((label != null) ? label.Trim().Length > 0 : true); 
            Contract.Requires<ArgumentException>((label != null) ? label.Trim().Length > 0 : true);

            return _client.GetRequest().Request("account/identity/" + id, new { label = label, status= status, master = master }, Method.PUT);
        }

        public RestAPIResponse Delete(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>((id != null) ? id.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((id != null) ? Constants.IDENTITY_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_IDENTITY_ID);

            return _client.GetRequest().Request("account/identity/" + id, null, Method.DELETE);
        }

    }
}
