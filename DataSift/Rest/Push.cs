using DataSift.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace DataSift.Rest
{
    public class Push
    {
        DataSiftClient _client = null;

        internal Push(DataSiftClient client)
        {
            _client = client;
        }

        public RestAPIResponse Get(string id = null, string hash = null, string historicsId = null, int? page = null, int? perPage = null, OrderBy? orderBy = null, OrderDirection? orderDirection = null, bool? includeFinished = null)
        {
            Contract.Requires<ArgumentException>((id != null) ? id.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SUBSCRIPTION_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SUBSCRIPTION_ID);
            Contract.Requires<ArgumentException>((hash != null) ? hash.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((hash != null) ? Constants.STREAM_HASH_FORMAT.IsMatch(hash) : true, Messages.INVALID_STREAM_HASH);
            Contract.Requires<ArgumentException>((historicsId != null) ? historicsId.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((historicsId != null) ? Constants.HISTORICS_ID_FORMAT.IsMatch(historicsId) : true, Messages.INVALID_HISTORICS_ID);
            Contract.Requires<ArgumentException>((page.HasValue) ? page.Value > 0 : true);
            Contract.Requires<ArgumentException>((perPage.HasValue) ? perPage.Value > 0 : true);
            
            return _client.GetRequest().Request("push/get", new { id = id, hash = hash, historics_id = historicsId, page = page, per_page = perPage, order_by = orderBy, order_dir = orderDirection, include_finished = includeFinished });
        }

        public RestAPIResponse Validate(string outputType)
        {
            return Validate(outputType, null);
        }

        public RestAPIResponse Validate(string outputType, dynamic outputParameters)
        {
            Contract.Requires<ArgumentNullException>(outputType != null);
            Contract.Requires<ArgumentException>(outputType.Trim().Length > 0);

            return _client.GetRequest().Request("push/validate", new { output_type = outputType, output_params = outputParameters }, Method.POST);
        }

        public RestAPIResponse Create(string name, string outputType, string hash = null, string historicsId = null, PushStatus? initialStatus = null, DateTimeOffset? start = null, DateTimeOffset? end = null)
        {
            return Create(name, outputType, null, hash, historicsId, initialStatus, start, end);
        }
        public RestAPIResponse Create(string name, string outputType, dynamic outputParameters, string hash = null, string historicsId = null, PushStatus? initialStatus = null, DateTimeOffset? start = null, DateTimeOffset? end = null)
        {
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name.Trim().Length > 0);
            Contract.Requires<ArgumentNullException>(outputType != null);
            Contract.Requires<ArgumentException>(outputType.Trim().Length > 0);
            Contract.Requires<ArgumentException>(hash != null || historicsId != null, Messages.PUSH_MUST_PROVIDE_HASH_OR_HISTORIC);
            Contract.Requires<ArgumentException>(hash == null || historicsId == null, Messages.PUSH_ONLY_HASH_OR_HISTORIC);
            Contract.Requires<ArgumentException>((hash != null) ? hash.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((hash != null) ? Constants.STREAM_HASH_FORMAT.IsMatch(hash) : true, Messages.INVALID_STREAM_HASH);
            Contract.Requires<ArgumentException>((historicsId != null) ? historicsId.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((historicsId != null) ? Constants.HISTORICS_ID_FORMAT.IsMatch(historicsId) : true, Messages.INVALID_HISTORICS_ID);
            Contract.Requires<ArgumentException>((end != null && start != null) ? end > start : true, Messages.HISTORICS_START_MUST_BE_BEFORE_END);

            return _client.GetRequest().Request("push/create", new { name = name, output_type = outputType, output_params = outputParameters, hash = hash, historics_id = historicsId, 
                initial_status = initialStatus, start = start, end = end }, Method.POST);
        }

        public RestAPIResponse Delete(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SUBSCRIPTION_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SUBSCRIPTION_ID);
            
            return _client.GetRequest().Request("push/delete", new { id = id }, Method.DELETE);
        }

        public RestAPIResponse Stop(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SUBSCRIPTION_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SUBSCRIPTION_ID);

            return _client.GetRequest().Request("push/stop", new {id = id}, Method.PUT);
        }

        public RestAPIResponse Pause(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SUBSCRIPTION_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SUBSCRIPTION_ID);

            return _client.GetRequest().Request("push/pause", new { id = id }, Method.PUT);
        }

        public RestAPIResponse Resume(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SUBSCRIPTION_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SUBSCRIPTION_ID);

            return _client.GetRequest().Request("push/resume", new { id = id }, Method.PUT);
        }

        public RestAPIResponse Log(string id = null, int? page = null, int? perPage = null, OrderDirection? orderDirection = null) 
        {
            Contract.Requires<ArgumentException>((id != null) ? id.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SUBSCRIPTION_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SUBSCRIPTION_ID);
            Contract.Requires<ArgumentException>((page.HasValue) ? page.Value > 0 : true);
            Contract.Requires<ArgumentException>((perPage.HasValue) ? perPage.Value > 0 : true);

            return _client.GetRequest().Request("push/log", new { id = id, page = page, perPage = perPage, order_dir = orderDirection });
        }

        public RestAPIResponse Update(string id, string name = null) 
        {
            return Update(id, null, name);
        }

        public RestAPIResponse Update(string id, dynamic outputParameters, string name = null) 
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.SUBSCRIPTION_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_SUBSCRIPTION_ID);

            return _client.GetRequest().Request("push/update", new { id = id, name = name, output_params = outputParameters }, Method.PUT);
        }
    }
}
