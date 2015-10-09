using DataSift;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSiftExamples
{
    static class Ingestion
    {
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            Console.WriteLine("Running 'Ingestion' example...");

            var response = client.ODP.Ingest("d44757b030a1499492f539591d86fe3e", new[] {
                    new { 
                        title = "Dummy content"
                    }
                });

            Console.WriteLine("\nIngestion response: " + JsonConvert.SerializeObject(response.Data));

        }
    }
}
