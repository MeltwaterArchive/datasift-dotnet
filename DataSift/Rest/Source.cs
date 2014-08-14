using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataSift.Rest
{
    public class Source
    {
        DataSiftClient _client = null;

        internal Source(DataSiftClient client)
        {
            _client = client;
        }

        public RestAPIResponse Start(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SOURCE_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SOURCE_ID);

            return _client.GetRequest().Request("source/start", new { id = id }, Method.PUT);
        }

        public RestAPIResponse Create(string sourceType, string name, dynamic parameters, dynamic resources, dynamic auth)
        {
            Contract.Requires<ArgumentNullException>(sourceType != null);
            Contract.Requires<ArgumentException>(sourceType.Trim().Length > 0);
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name.Trim().Length > 0);

            return _client.GetRequest().Request("source/create", new { source_type = sourceType, name = name, parameters = parameters, resources = resources, auth = auth } );
        }
        public RestAPIResponse Stop(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SOURCE_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SOURCE_ID);

            return _client.GetRequest().Request("source/stop", new { id = id }, Method.PUT);
        }

        public RestAPIResponse Log(string id, int? page = null, int? perPage = null)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SOURCE_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SOURCE_ID);
            Contract.Requires<ArgumentException>((page.HasValue) ? page.Value > 0 : true);
            Contract.Requires<ArgumentException>((perPage.HasValue) ? perPage.Value > 0 : true);

            return _client.GetRequest().Request("source/log", new { id = id, page = page, per_page = perPage });
        }

        public RestAPIResponse Update(string id, string sourceType = null, string name = null, dynamic parameters = null, dynamic resources = null, dynamic auth = null)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SOURCE_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SOURCE_ID);

            Contract.Requires<ArgumentException>((sourceType != null) ? sourceType.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((name != null) ? name.Trim().Length > 0 : true);

            return _client.GetRequest().Request("source/update", new { source_type = sourceType, name = name, id = id, parameters = parameters, resources = resources, auth = auth });
        }

        public RestAPIResponse Delete(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SOURCE_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SOURCE_ID);

            return _client.GetRequest().Request("source/delete", new { id = id }, Method.DELETE);
        }

        public RestAPIResponse Get(string sourceType = null, int? page = null, int? perPage = null, string id = null)
        {
            Contract.Requires<ArgumentException>((id != null) ? id.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SOURCE_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SOURCE_ID);
            Contract.Requires<ArgumentException>((sourceType != null) ? sourceType.Trim().Length > 0 : true); 
            Contract.Requires<ArgumentException>((page.HasValue) ? page.Value > 0 : true);
            Contract.Requires<ArgumentException>((perPage.HasValue) ? perPage.Value > 0 : true);

            return _client.GetRequest().Request("source/get", new { source_type = sourceType, page = page, per_page = perPage, id = id });
        }

        public RestAPIResponse ResourceAdd(string id, dynamic resources, bool? validate = null)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>((id != null) ? id.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SOURCE_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SOURCE_ID);

            return _client.GetRequest().Request("source/resource/add", new { id = id, resources = resources, validate = validate });
        }

        public RestAPIResponse ResourceRemove(string id, string[] resourceIds)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>((id != null) ? id.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SOURCE_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SOURCE_ID);

            Contract.Requires<ArgumentNullException>(resourceIds != null);
            Contract.Requires<ArgumentException>(resourceIds.Length > 0);

            return _client.GetRequest().Request("source/resource/remove", new { id = id, resource_ids = resourceIds });
        }

        public RestAPIResponse AuthAdd(string id, dynamic auth, bool? validate = null)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>((id != null) ? id.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SOURCE_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SOURCE_ID);

            return _client.GetRequest().Request("source/auth/add", new { id = id, auth = auth, validate = validate });
        }

        public RestAPIResponse AuthRemove(string id, string[] authIds)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>((id != null) ? id.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SOURCE_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SOURCE_ID);

            Contract.Requires<ArgumentNullException>(authIds != null);
            Contract.Requires<ArgumentException>(authIds.Length > 0);

            return _client.GetRequest().Request("source/auth/remove", new { id = id, auth_ids = authIds });
        }

    }
}
