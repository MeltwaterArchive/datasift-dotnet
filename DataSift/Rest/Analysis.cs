using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using RestSharp;

namespace DataSift.Rest
{
    public class Analysis
    {
        DataSiftClient _client = null;

        internal Analysis(DataSiftClient client)
        {
            _client = client;
        }

        public RestAPIResponse Get(string hash = null)
        {
            Contract.Requires<ArgumentException>((hash != null) ? hash.Trim().Length > 0 : true);
            Contract.Requires<ArgumentException>((hash != null) ? Constants.STREAM_HASH_FORMAT.IsMatch(hash) : true, Messages.INVALID_STREAM_HASH);

            return _client.GetRequest().Request("analysis/get", new { hash = hash });
        }

        public RestAPIResponse Validate(string csdl)
        {
            Contract.Requires<ArgumentNullException>(csdl != null);
            Contract.Requires<ArgumentException>(csdl.Trim().Length > 0);

            return _client.GetRequest().Request("analysis/validate", new { csdl = csdl }, Method.POST);
        }

        public RestAPIResponse Compile(string csdl)
        {
            Contract.Requires<ArgumentNullException>(csdl != null);
            Contract.Requires<ArgumentException>(csdl.Trim().Length > 0);

            return _client.GetRequest().Request("analysis/compile", new { csdl = csdl }, Method.POST);
        }

        public RestAPIResponse Start(string hash, string name)
        {
            Contract.Requires<ArgumentNullException>(hash != null);
            Contract.Requires<ArgumentException>(hash.Trim().Length > 0);

            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name.Trim().Length > 0);

            return _client.GetRequest().Request("analysis/start", new { hash = hash, name = name }, Method.POST);
        }

        public RestAPIResponse Stop(string hash)
        {
            Contract.Requires<ArgumentNullException>(hash != null);
            Contract.Requires<ArgumentException>(hash.Trim().Length > 0);

            return _client.GetRequest().Request("analysis/stop", new { hash = hash }, Method.POST);
        }

        public RestAPIResponse Analyze(string hash, dynamic parameters, string filter = null, DateTimeOffset? start = null, DateTimeOffset? end = null, bool? includeParametersInReply = false)
        {
            Contract.Requires<ArgumentNullException>(hash != null);
            Contract.Requires<ArgumentException>(hash.Trim().Length > 0);
            Contract.Requires<ArgumentException>((filter != null) ? filter.Trim().Length > 0 : true);

            Contract.Requires<ArgumentException>((start != null) ? start <= DateTimeOffset.Now : true, Messages.ANALYSIS_START_TOO_LATE);
            Contract.Requires<ArgumentException>((end != null) ? end <= DateTimeOffset.Now : true, Messages.ANALYSIS_END_TOO_LATE);
            Contract.Requires<ArgumentException>((end != null && start != null) ? end > start : true, Messages.ANALYSIS_START_MUST_BE_BEFORE_END);

            return _client.GetRequest().Request("analysis/analyze", new { hash = hash, parameters = parameters, filter = filter, start = start, end = end, includeParametersInReply = includeParametersInReply }, Method.POST);
        }

    }
}
