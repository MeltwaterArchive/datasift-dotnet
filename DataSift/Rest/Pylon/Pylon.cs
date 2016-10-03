using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using RestSharp;
using System.Dynamic;
using RestSharp.Contrib;

namespace DataSift.Rest.Pylon
{
    public class Pylon
    {
        DataSiftClient _client = null;
        private Task _task;

        internal Pylon(DataSiftClient client)
        {
            _client = client;
        }


        public Task Task
        {
            get
            {
                if (_task == null) _task = new Task(_client);
                return _task;
            }
        }

        public RestAPIResponse Get(string service, string id = null, int? page = null, int? perPage = null)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            Contract.Requires<ArgumentException>((id != null) ? id.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((id != null) ? Constants.RECORDING_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_RECORDING_ID);
            Contract.Requires<ArgumentException>((page.HasValue) ? page.Value > 0 : true);
            Contract.Requires<ArgumentException>((perPage.HasValue) ? perPage.Value > 0 : true);

            return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/get", new { id = id });
        }

        public RestAPIResponse Validate(string service, string csdl)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            Contract.Requires<ArgumentNullException>(csdl != null);
            Contract.Requires<ArgumentException>(csdl.Trim().Length > 0);

            return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/validate", new { csdl = csdl }, Method.POST);
        }

        public RestAPIResponse Compile(string service, string csdl)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            Contract.Requires<ArgumentNullException>(csdl != null);
            Contract.Requires<ArgumentException>(csdl.Trim().Length > 0);

            return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/compile", new { csdl = csdl }, Method.POST);
        }

        public RestAPIResponse Start(string service, string hash = null, string name = null, string id = null)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            Contract.Requires<ArgumentException>((hash != null) ? hash.Trim().Length > 0: true);
            Contract.Requires<ArgumentException>((hash != null) ? Constants.STREAM_HASH_FORMAT.IsMatch(hash) : true, Messages.INVALID_STREAM_HASH);

            Contract.Requires<ArgumentException>((name != null) ? name.Trim().Length > 0 : true);

            Contract.Requires<ArgumentException>((id != null) ? id.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((id != null) ? Constants.RECORDING_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_RECORDING_ID);

            return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/start", new { hash = hash, name = name, id = id }, Method.PUT);
        }

        public RestAPIResponse Stop(string service, string id)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>((id != null) ? Constants.RECORDING_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_RECORDING_ID);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);

            return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/stop", new { id = id }, Method.PUT);
        }

        public RestAPIResponse Update(string service, string id, string hash = null, string name = null)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>((id != null) ? Constants.RECORDING_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_RECORDING_ID);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);

            Contract.Requires<ArgumentException>((hash != null) ? hash.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((hash != null) ? Constants.STREAM_HASH_FORMAT.IsMatch(hash) : true, Messages.INVALID_STREAM_HASH);

            Contract.Requires<ArgumentException>((name != null) ? name.Trim().Length > 0 : true);

            return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/update", new { id =id, hash = hash, name = name }, Method.PUT);
        }

        public RestAPIResponse Analyze(string service, string id, dynamic parameters, string filter = null, DateTimeOffset? start = null, DateTimeOffset? end = null)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>((id != null) ? Constants.RECORDING_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_RECORDING_ID);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((filter != null) ? filter.Trim().Length > 0 : true);

            Contract.Requires<ArgumentException>((start != null) ? start <= DateTimeOffset.Now : true, Messages.ANALYSIS_START_TOO_LATE);
            Contract.Requires<ArgumentException>((end != null) ? end <= DateTimeOffset.Now : true, Messages.ANALYSIS_END_TOO_LATE);
            Contract.Requires<ArgumentException>((end != null && start != null) ? end > start : true, Messages.ANALYSIS_START_MUST_BE_BEFORE_END);

            if(ReferenceEquals(null, parameters))
            {
                throw new ArgumentNullException("parameters");
            }

            return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/analyze", new { id = id, parameters = parameters, filter = filter, start = start, end = end }, Method.POST);
        }

        public RestAPIResponse Tags(string service, string id)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>((id != null) ? Constants.RECORDING_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_RECORDING_ID);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);

            return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/tags", new { id = id });
        }

        public RestAPIResponse Sample(string service, string id, int? count = null, DateTimeOffset? start = null, DateTimeOffset? end = null, string filter = null)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            Contract.Requires<ArgumentException>((service != null) ? service.Trim().Length > 0 : true);

            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>(Constants.RECORDING_ID_FORMAT.IsMatch(id), Messages.INVALID_RECORDING_ID);
            Contract.Requires<ArgumentException>((count != null) ? count >= 10 : true, Messages.INVALID_COUNT);
            Contract.Requires<ArgumentException>((count != null) ? count <= 100 : true, Messages.INVALID_COUNT);

            Contract.Requires<ArgumentException>((start != null) ? start <= DateTimeOffset.Now : true, Messages.ANALYSIS_START_TOO_LATE);
            Contract.Requires<ArgumentException>((end != null) ? end <= DateTimeOffset.Now : true, Messages.ANALYSIS_END_TOO_LATE);
            Contract.Requires<ArgumentException>((end != null && start != null) ? end > start : true, Messages.ANALYSIS_START_MUST_BE_BEFORE_END);

            Contract.Requires<ArgumentException>((filter != null) ? filter.Trim().Length > 0 : true);

            return _client.GetRequest().Request("pylon/" + HttpUtility.UrlEncode(service) + "/sample", new { id = id, count = count, start = start, end = end, filter = filter });
        }

    }
}
