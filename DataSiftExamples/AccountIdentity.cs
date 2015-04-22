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

            var identity = client.Account.Identity.Create("Test Identity");
            Console.WriteLine("\nCreated new identity: " + identity.Data.id);

            var identityRenamed = client.Account.Identity.Update(identity.Data.id, "Test Identity Renamed");
            Console.WriteLine("\nUpdated identity: " + identityRenamed.Data.label);

            var getById = client.Account.Identity.Get(id: identity.Data.id);
            Console.WriteLine("\nGot updated identity: " + JsonConvert.SerializeObject(getById.Data));

            client.Account.Identity.Delete(id: identity.Data.id);
            Console.WriteLine("\nDeleted identity.");

        }

    }
}
