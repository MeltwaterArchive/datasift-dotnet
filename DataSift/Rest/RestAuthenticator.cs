using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Rest
{
    /// <summary>
    /// Alternative authenticator to the BasicHttpAuthenticator in RestSharp. It removes the Base64 encoding of auth details as this is not required by the API, and we recommend all requests are made over HTTPS.
    /// </summary>
    internal class RestAuthenticator : IAuthenticator
    {
        private readonly string authHeader;

        public RestAuthenticator(string username, string password)
        {
            this.authHeader = string.Format("{0}:{1}", username, password);
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            if (!request.Parameters.Any(p => p.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase)))
            {
                request.AddParameter("Authorization", this.authHeader, ParameterType.HttpHeader);
            }
        }
    }
}
