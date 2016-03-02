using DataSift;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSiftExamples
{
    static class Pylon
    {
        // Note that to run the PYLON example you must use an API key which corresponds to a valid identity with Facebook access
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            Console.WriteLine("Running 'Pylon' example...");

            var csdlV1 = "fb.content contains_any \"Ford, BMW, Honda\"";
            var csdlV2 = "fb.content contains_any \"Ford, BMW, Honda, Mercedes\"";

            var get = client.Pylon.Get();
            Console.WriteLine("\nCurrent of recordings / tasks: " + JsonConvert.SerializeObject(get.Data));

            client.Pylon.Validate(csdlV1);
            Console.WriteLine("CSDL for recording validated");

            var compile = client.Pylon.Compile(csdlV1);
            Console.WriteLine("Hash for filter: " + compile.Data.hash);

            var start = client.Pylon.Start(compile.Data.hash, "Example recording");
            Console.WriteLine("Recording started");

            Console.WriteLine("\nSleeping for a few seconds...");
            Thread.Sleep(5000);

            var getRecording = client.Pylon.Get(id: start.Data.id);
            Console.WriteLine("\nThis recording: " + JsonConvert.SerializeObject(getRecording.Data));

            var analysisParams = new  {
                    analysis_type = "freqDist",
                    parameters = new
                    {
                        threshold = 5,
                        target = "fb.author.age"
                    }
                };

            var analysis = client.Pylon.Analyze(start.Data.id, analysisParams);
            Console.WriteLine("\nAnalysis result: " + JsonConvert.SerializeObject(analysis.Data));

            var analysisWithFilter = client.Pylon.Analyze(start.Data.id, analysisParams, filter: "fb.author.gender == \"male\"");
            Console.WriteLine("\nAnalysis (with filter) result: " + JsonConvert.SerializeObject(analysisWithFilter.Data));

            dynamic nested = new
            {
                analysis_type = "freqDist",
                parameters = new
                {
                    threshold = 3,
                    target = "fb.author.gender"
                },
                child = new
                {
                    analysis_type = "freqDist",
                    parameters = new
                    {
                        threshold = 3,
                        target = "fb.author.age"
                    }
                }
            };

            var analysisNested = client.Pylon.Analyze(start.Data.id, nested);
            Console.WriteLine("\nNested analysis result: " + JsonConvert.SerializeObject(analysisNested.Data));

            var compile2 = client.Pylon.Compile(csdlV2);
            Console.WriteLine("\nHash for updated filter: " + compile2.Data.hash);

            var update = client.Pylon.Update(start.Data.id, hash: compile2.Data.hash, name: "Example recording - updated");
            Console.WriteLine("\nRecording updated");

            Console.WriteLine("\nSleeping for a few seconds...");
            Thread.Sleep(5000);

            getRecording = client.Pylon.Get(id: start.Data.id);
            Console.WriteLine("\nThis recording: " + JsonConvert.SerializeObject(getRecording.Data));

            var tags = client.Pylon.Tags(start.Data.id);
            Console.WriteLine("\nTags: " + JsonConvert.SerializeObject(tags.Data));

            var sample = client.Pylon.Sample(start.Data.id, count: 10);
            Console.WriteLine("\nSuper public samples: " + sample.ToJson());

            client.Pylon.Stop(start.Data.id);
            Console.WriteLine("\nRecording stopped");

        }
    }
}
