using System;
using System.Collections.Generic;
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

        public RestAPIResponse Create(string identityId, string service, int totalAllowance)
        {
            throw new NotImplementedException();
        }

        public RestAPIResponse Get(string identityId, string service = null, int? page = null, int? perPage = null)
        {
            throw new NotImplementedException();
        }

        public RestAPIResponse Update(string identityId, string service, int totalAllowance)
        {
            throw new NotImplementedException();
        }

        public RestAPIResponse Delete(string identityId, string service)
        {
            throw new NotImplementedException();
        }
    }
}
