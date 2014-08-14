using DataSift;
using DataSift.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSiftExamples
{
    static class HistoricsPreview
    {
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            Console.WriteLine("Running 'Historics Preview' example...");

            var compiled = client.Compile("interaction.content contains \"datasift\"");
            Console.WriteLine("\nCompiled to {0}", compiled.Data.hash);

            var prms = new List<HistoricsPreviewParameter>();
            prms.Add(new HistoricsPreviewParameter() { Target = "interaction.author.link", Analysis = "targetVol", Argument = "hour" });
            prms.Add(new HistoricsPreviewParameter() { Target = "twitter.user.lang", Analysis = "freqDist", Argument = "10" });
            prms.Add(new HistoricsPreviewParameter() { Target = "twitter.user.followers_count", Analysis = "numStats", Argument = "hour" });
            prms.Add(new HistoricsPreviewParameter() { Target = "interaction.content", Analysis = "wordCount", Argument = "10" });

            var create = client.HistoricsPreview.Create(compiled.Data.hash, new string[] { "twitter" }, prms, DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now.AddDays(-1));
            Console.WriteLine("\nCreated preview: {0}", create.Data.id);

            Console.WriteLine("\nPausing for preview status update.");
            Thread.Sleep(10000);

            var get1 = client.HistoricsPreview.Get(create.Data.id);
            Console.WriteLine("\nPreview status: " + JsonConvert.SerializeObject(get1.Data));
        }
    }
}
