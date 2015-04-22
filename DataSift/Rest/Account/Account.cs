using DataSift.Rest.Account;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest.Account
{
    public class Account
    {
        DataSiftClient _client = null;

        private Identity _identity;

        internal Account(DataSiftClient client)
        {
            _client = client;
        }

        public Identity Identity
        {
            get
            {
                if (_identity == null) _identity = new Identity(_client);
                return _identity;
            }
        }
    }
}
