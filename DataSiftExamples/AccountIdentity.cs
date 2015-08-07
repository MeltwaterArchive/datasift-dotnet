using DataSift;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSiftExamples
{
    static class AccountIdentity
    {
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            Console.WriteLine("Running 'Account Identity' example...");

            var get = client.Account.Identity.Get();
            Console.WriteLine("\nGot current identities: " + JsonConvert.SerializeObject(get.Data));

            var identity = client.Account.Identity.Create("Test Identity " + DateTime.Now.Ticks.ToString());
            Console.WriteLine("\nCreated new identity: " + identity.Data.id);

            var identityRenamed = client.Account.Identity.Update(identity.Data.id, "Test Identity Renamed " + DateTime.Now.Ticks.ToString());
            Console.WriteLine("\nUpdated identity: " + identityRenamed.Data.label);

            var getById = client.Account.Identity.Get(id: identity.Data.id);
            Console.WriteLine("\nGot updated identity: " + JsonConvert.SerializeObject(getById.Data));

            var token = client.Account.Identity.Token.Create(identity.Data.id, "facebook", "ddd85f6b316fb18930ee28e8754f4963");
            Console.WriteLine("\nCreated token: " + JsonConvert.SerializeObject(token.Data));

            var getAllIdentityTokens = client.Account.Identity.Token.Get(identity.Data.id);
            Console.WriteLine("\nAll tokens for identity: " + JsonConvert.SerializeObject(getAllIdentityTokens.Data));

            var getTokenByService = client.Account.Identity.Token.Get(identity.Data.id, "facebook");
            Console.WriteLine("\nGot token by service: " + JsonConvert.SerializeObject(getTokenByService.Data));

            var updatedToken = client.Account.Identity.Token.Update(identity.Data.id, "facebook", "eed85f6b316fb18930ee28e8754f4963");
            Console.WriteLine("\nUpdated token: " + JsonConvert.SerializeObject(updatedToken.Data));

            var limit = client.Account.Identity.Limit.Create(identity.Data.id, "facebook", 100000);
            Console.WriteLine("\nCreated limit: " + JsonConvert.SerializeObject(limit.Data));

            var allLimitsForService = client.Account.Identity.Limit.Get("facebook");
            Console.WriteLine("\nAll limits for service: " + JsonConvert.SerializeObject(allLimitsForService.Data));

            var limitForService = client.Account.Identity.Limit.Get("facebook", identity.Data.id);
            Console.WriteLine("\nLimit for service: " + JsonConvert.SerializeObject(limitForService.Data));

            var updatedLimit = client.Account.Identity.Limit.Update(identity.Data.id, "facebook", 200000);
            Console.WriteLine("\nUpdated limit: " + JsonConvert.SerializeObject(updatedLimit.Data));

            client.Account.Identity.Limit.Delete(identity.Data.id, "facebook");
            Console.WriteLine("\nDeleted limit.");

            client.Account.Identity.Token.Delete(identity.Data.id, "facebook");
            Console.WriteLine("\nDeleted token.");

            // DELETE not currently supported by the API
            //client.Account.Identity.Delete(id: identity.Data.id);
            //Console.WriteLine("\nDeleted identity.");

        }

    }
}
