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
    public class HistoricsPreview
    {
        DataSiftClient _client = null;

        internal HistoricsPreview(DataSiftClient client)
        {
            _client = client;
        }
        public RestAPIResponse Create(string hash, string[] sources, List<HistoricsPreviewParameter> parameters, DateTimeOffset start, DateTimeOffset? end = null)
        {
            Contract.Requires<ArgumentNullException>(hash != null);
            Contract.Requires<ArgumentException>(hash.Trim().Length > 0);
            Contract.Requires<ArgumentException>(Constants.STREAM_HASH_FORMAT.IsMatch(hash), Messages.INVALID_STREAM_HASH);

            Contract.Requires<ArgumentNullException>(sources != null);
            Contract.Requires<ArgumentException>(sources.Length > 0);

            Contract.Requires<ArgumentException>(start >= new DateTimeOffset(2010, 1, 1, 0, 0, 0, TimeSpan.Zero), Messages.HISTORICS_START_TOO_EARLY);
            Contract.Requires<ArgumentException>(start <= DateTimeOffset.Now.AddHours(-2), Messages.HISTORICS_START_TOO_LATE);
            Contract.Requires<ArgumentException>((end.HasValue) ? end <= DateTimeOffset.Now.AddHours(-1) : true, Messages.HISTORICS_END_TOO_LATE);
            Contract.Requires<ArgumentException>((end.HasValue) ? end > start : true, Messages.HISTORICS_START_MUST_BE_BEFORE_END);

            Contract.Requires<ArgumentNullException>(parameters != null);
            Contract.Requires<ArgumentException>(parameters.Count > 0);
            Contract.Requires<ArgumentException>(parameters.Count <= 20);

            return _client.GetRequest().Request("preview/create", new { hash = hash, sources = sources, parameters = parameters, start = start, end = end }, Method.POST);
        }

        public RestAPIResponse Get(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id.Trim().Length > 0);
            Contract.Requires<ArgumentException>((id != null) ? Constants.HISTORICS_PREVIEW_ID_FORMAT.IsMatch(id) : true, Messages.INVALID_HISTORICS_PREVIEW_ID);

            return _client.GetRequest().Request("preview/get", new { id= id });
        }

    }
}
