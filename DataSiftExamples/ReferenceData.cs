using DataSift;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSiftExamples
{
    static class ReferenceData
    {
        // Note that to run the example you must use an API key which corresponds to a valid identity with Media Strategies API access
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            // TODO: Insert the service and slug you'd like to use
            var service = "";
            var slug = "";

            Console.WriteLine("Running 'Reference Data API' example...");

            var get = client.Pylon.Reference.Get(service);
            Console.WriteLine("\nAvailable data sets: " + JsonConvert.SerializeObject(get.Data));

            var getOne = client.Pylon.Reference.Get(service, slug: slug);
            Console.WriteLine("\nRetrieved data set: " + JsonConvert.SerializeObject(getOne.Data));

        }
    }
}
