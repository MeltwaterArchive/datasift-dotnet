using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest
{
    public class ODP
    {
        DataSiftClient _client = null;

        internal ODP(DataSiftClient client)
        {
            _client = client;
        }

        public RestAPIResponse Ingest(string sourceId, dynamic data)
        {
            Contract.Requires<ArgumentNullException>(sourceId != null);
            Contract.Requires<ArgumentException>(sourceId.Trim().Length > 0);
            Contract.Requires<ArgumentException>((sourceId != null) ? Constants.SOURCE_ID_FORMAT.IsMatch(sourceId) : true, Messages.INVALID_SOURCE_ID);

            if (data == null)
                throw new ArgumentNullException("data", "data parameter cannot be null");
            
            if (!(data.GetType().IsArray))
                throw new ArgumentException("data", "data parameter must be an array of objects");
            
            return _client.GetIngestRequest().Ingest(sourceId, data);
        }
    }
}
