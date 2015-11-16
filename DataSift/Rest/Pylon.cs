using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using RestSharp;
using System.Dynamic;

namespace DataSift.Rest
{
    public class Pylon
    {
        DataSiftClient _client = null;

        internal Pylon(DataSiftClient client)
        {
            _client = client;
        }

        public RestAPIResponse Get(string hash = null, int? page = null, int? perPage = null)
        {
            Contract.Requires<ArgumentException>((hash != null) ? hash.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((hash != null) ? Constants.STREAM_HASH_FORMAT.IsMatch(hash) : true, Messages.INVALID_STREAM_HASH);
            Contract.Requires<ArgumentException>((page.HasValue) ? page.Value > 0 : true);
            Contract.Requires<ArgumentException>((perPage.HasValue) ? perPage.Value > 0 : true);

            return _client.GetRequest().Request("pylon/get", new { hash = hash });
        }

        public RestAPIResponse Validate(string csdl)
        {
            Contract.Requires<ArgumentNullException>(csdl != null);
            Contract.Requires<ArgumentException>(csdl.Trim().Length > 0);

            return _client.GetRequest().Request("pylon/validate", new { csdl = csdl }, Method.POST);
        }

        public RestAPIResponse Compile(string csdl)
        {
            Contract.Requires<ArgumentNullException>(csdl != null);
            Contract.Requires<ArgumentException>(csdl.Trim().Length > 0);

            return _client.GetRequest().Request("pylon/compile", new { csdl = csdl }, Method.POST);
        }

        public RestAPIResponse Start(string hash, string name)
        {
            Contract.Requires<ArgumentNullException>(hash != null);
            Contract.Requires<ArgumentException>(hash.Trim().Length > 0);

            Contract.Requires<ArgumentException>((name != null) ? name.Trim().Length > 0 : true);

            return _client.GetRequest().Request("pylon/start", new { hash = hash, name = name }, Method.POST);
        }

        public RestAPIResponse Stop(string hash)
        {
            Contract.Requires<ArgumentNullException>(hash != null);
            Contract.Requires<ArgumentException>(hash.Trim().Length > 0);

            return _client.GetRequest().Request("pylon/stop", new { hash = hash }, Method.POST);
        }

        public RestAPIResponse Analyze(string hash, dynamic parameters, string filter = null, DateTimeOffset? start = null, DateTimeOffset? end = null)
        {
            Contract.Requires<ArgumentNullException>(hash != null);
            Contract.Requires<ArgumentException>(hash.Trim().Length > 0);
            Contract.Requires<ArgumentException>((filter != null) ? filter.Trim().Length > 0 : true);

            Contract.Requires<ArgumentException>((start != null) ? start <= DateTimeOffset.Now : true, Messages.ANALYSIS_START_TOO_LATE);
            Contract.Requires<ArgumentException>((end != null) ? end <= DateTimeOffset.Now : true, Messages.ANALYSIS_END_TOO_LATE);
            Contract.Requires<ArgumentException>((end != null && start != null) ? end > start : true, Messages.ANALYSIS_START_MUST_BE_BEFORE_END);

            if(ReferenceEquals(null, parameters))
            {
                throw new ArgumentNullException("parameters");
            }

            return _client.GetRequest().Request("pylon/analyze", new { hash = hash, parameters = parameters, filter = filter, start = start, end = end }, Method.POST);
        }

        public RestAPIResponse Tags(string hash)
        {
            Contract.Requires<ArgumentNullException>(hash != null);
            Contract.Requires<ArgumentException>(hash.Trim().Length > 0);

            return _client.GetRequest().Request("pylon/tags", new { hash = hash });
        }

        public RestAPIResponse Sample(string hash, int? count = null, DateTimeOffset? start = null, DateTimeOffset? end = null, string filter = null)
        {
            Contract.Requires<ArgumentNullException>(hash != null);
            Contract.Requires<ArgumentException>(hash.Trim().Length > 0);
            Contract.Requires<ArgumentException>(Constants.STREAM_HASH_FORMAT.IsMatch(hash), Messages.INVALID_STREAM_HASH);
            Contract.Requires<ArgumentException>((count != null) ? count >= 10 : true, Messages.INVALID_COUNT);
            Contract.Requires<ArgumentException>((count != null) ? count <= 100 : true, Messages.INVALID_COUNT);

            Contract.Requires<ArgumentException>((start != null) ? start <= DateTimeOffset.Now : true, Messages.ANALYSIS_START_TOO_LATE);
            Contract.Requires<ArgumentException>((end != null) ? end <= DateTimeOffset.Now : true, Messages.ANALYSIS_END_TOO_LATE);
            Contract.Requires<ArgumentException>((end != null && start != null) ? end > start : true, Messages.ANALYSIS_START_MUST_BE_BEFORE_END);

            Contract.Requires<ArgumentException>((filter != null) ? filter.Trim().Length > 0 : true);

            return _client.GetRequest().Request("pylon/sample", new { hash = hash, count = count, start = start, end = end, filter = filter });
        }

    }
}
