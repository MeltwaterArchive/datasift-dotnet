using DataSift;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataSiftExamples
{
    static class Pylon
    {
        // Note that to run the PYLON example you must use an API key which corresponds to a valid identity with Facebook access
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            var csdl = "fb.content contains_any \"BMW, Mercedes, Cadillac\"";

            Console.WriteLine("Running 'Pylon' example...");

            var get = client.Pylon.Get();
            Console.WriteLine("\nCurrent of recordings / tasks: " + JsonConvert.SerializeObject(get.Data));

            client.Pylon.Validate(csdl);
            Console.WriteLine("CSDL for recording validated");

            var compile = client.Pylon.Compile(csdl);
            Console.WriteLine("Hash for stream: " + compile.Data.hash);

            client.Pylon.Start(compile.Data.hash, "Example recording");
            Console.WriteLine("Recording started");

            var getRecording = client.Pylon.Get(hash: compile.Data.hash);
            Console.WriteLine("\nThis recording: " + JsonConvert.SerializeObject(getRecording.Data));

            var analysisParams = new  {
                    analysis_type = "freqDist",
                    parameters = new
                    {
                        threshold = 5,
                        target = "fb.author.age"
                    }
                };

            var analysis = client.Pylon.Analyze(compile.Data.hash, analysisParams);
            Console.WriteLine("\nAnalysis result: " + JsonConvert.SerializeObject(analysis.Data));

            var analysisWithFilter = client.Pylon.Analyze(compile.Data.hash, analysisParams, filter: "fb.author.gender == \"male\"");
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

            var analysisNested = client.Pylon.Analyze(compile.Data.hash, nested);
            Console.WriteLine("\nNested analysis result: " + JsonConvert.SerializeObject(analysisNested.Data));

            var tags = client.Pylon.Tags(compile.Data.hash);
            Console.WriteLine("\nTags: " + JsonConvert.SerializeObject(tags.Data));

            var sample = client.Pylon.Sample(compile.Data.hash, count: 10);
            Console.WriteLine("\nSuper public samples: " + sample.ToJson());

            client.Pylon.Stop(compile.Data.hash);
            Console.WriteLine("\nRecording stopped");

        }
    }
}
