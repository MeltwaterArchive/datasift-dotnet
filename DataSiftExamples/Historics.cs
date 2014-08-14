using DataSift;
using DataSift.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSiftExamples
{
    static class Historics
    {
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            Console.WriteLine("Running 'Historics' example...");

            var get = client.Historics.Get();
            Console.WriteLine("\nList of historics: " + JsonConvert.SerializeObject(get.Data));

            var status = client.Historics.Status(DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now.AddDays(-1), new string[] { "twitter" });
            Console.WriteLine("\nTwitter status for period: " + status.Data[0].sources.twitter.status);

            var compiled = client.Compile("interaction.content contains \"datasift\"");
            Console.WriteLine("\nCompiled CSDL to {0}, DPU = {1}", compiled.Data.hash, compiled.Data.dpu);

            var prepare = client.Historics.Prepare(compiled.Data.hash, DateTimeOffset.Now.AddDays(-8), DateTimeOffset.Now.AddDays(-1), "Example historic", new string[] { "twitter" }, Sample.TenPercent);
            Console.WriteLine("\nPrepared historic query, ID = " + prepare.Data.id);

            var subscription = client.Push.Create("Example historic subscription", "pull", historicsId: prepare.Data.id);
            Console.WriteLine("\nCreated subscription, ID = " + subscription.Data.id);

            client.Historics.Start(prepare.Data.id);
            Console.WriteLine("\nStarted historic.");

            var update = client.Historics.Update(prepare.Data.id, "Updated historic query");
            Console.WriteLine("\nUpdated historic.");

            var getById = client.Historics.Get(id: prepare.Data.id);
            Console.WriteLine("\nDetails for updated historic: " + JsonConvert.SerializeObject(getById.Data));

            client.Historics.Pause(prepare.Data.id);
            Console.WriteLine("\nPaused historic.");

            client.Historics.Resume(prepare.Data.id);
            Console.WriteLine("Resumed historic.");

            client.Historics.Stop(prepare.Data.id);
            Console.WriteLine("Stopped historic.");

            client.Historics.Delete(prepare.Data.id);
            Console.WriteLine("Deleted historic.");
        }
    }
}
