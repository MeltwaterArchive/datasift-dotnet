using DataSift;
using DataSift.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSiftExamples
{
    static class Push
    {
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            Console.WriteLine("Running 'Push' example...");

            var get = client.Push.Get(page: 1, perPage: 5, orderBy: OrderBy.UpdatedAt, includeFinished: true);
            Console.WriteLine("\nList of push subscriptions: " + JsonConvert.SerializeObject(get.Data));

            var compiled = client.Compile("interaction.content contains \"music\"");
            Console.WriteLine("\nCompiled to {0}", compiled.Data.hash);

            var create = client.Push.Create(".NET example pull", "pull", hash: compiled.Data.hash);
            Console.WriteLine("\nCreated pull subscription: {0}", create.Data.id);

            var update = client.Push.Update(create.Data.id, name: "Updated example pull");
            Console.WriteLine("\nUpdated subscription name.");

            var getById = client.Push.Get(id: create.Data.id);
            Console.WriteLine("\nSubscription details: " + JsonConvert.SerializeObject(getById.Data));

            var log = client.Push.Log(create.Data.id);
            Console.WriteLine("\nLog for new subscription: " + JsonConvert.SerializeObject(log.Data.log_entries));

            Console.WriteLine("\nPausing for data.");
            Thread.Sleep(5000);

            var pull = client.Pull(create.Data.id, size: 500000);
            Console.WriteLine("\nGot data, first interaction: " + JsonConvert.SerializeObject(pull.Data[0]));

            client.Push.Stop(create.Data.id);
            Console.WriteLine("\nStopped subscription.");

            client.Push.Delete(create.Data.id);
            Console.WriteLine("\nDeleted subscription.");
        }
    }
}
