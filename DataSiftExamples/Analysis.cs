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
    static class Analysis
    {
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            var csdl = "fb.content contains_any \"BMW, Mercedes, Cadillac\"";

            Console.WriteLine("Running 'Analysis' example...");

            var get = client.Analysis.Get();
            Console.WriteLine("\nCurrent of recordings / tasks: " + JsonConvert.SerializeObject(get.Data));

            client.Analysis.Validate(csdl);
            Console.WriteLine("CSDL for recording validated");

            var compile = client.Analysis.Compile(csdl);
            Console.WriteLine("Hash for stream: " + compile.Data.hash);

            client.Analysis.Start(compile.Data.hash, "Example recording");
            Console.WriteLine("Recording started");

            var getRecording = client.Analysis.Get(hash: compile.Data.hash);
            Console.WriteLine("\nThis recording: " + JsonConvert.SerializeObject(get.Data));

            var analysisParams = new  {
                    analysis_type = "freqDist",
                    parameters = new
                    {
                        threshold = 5,
                        target = "fb.author.age"
                    }
                };

            var analysis = client.Analysis.Analyze(compile.Data.hash, analysisParams);
            Console.WriteLine("\nAnalysis result: " + JsonConvert.SerializeObject(analysis.Data));

            var analysisWithFilter = client.Analysis.Analyze(compile.Data.hash, analysisParams, filter: "fb.author.gender == \"male\"");
            Console.WriteLine("\nAnalysis (with filter) result: " + JsonConvert.SerializeObject(analysisWithFilter.Data));

            client.Analysis.Stop(compile.Data.hash);
            Console.WriteLine("Recording stopped");

        }
    }
}
