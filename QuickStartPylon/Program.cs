using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataSift;
using Newtonsoft.Json;
using DataSift.Rest;
using System.Threading;

namespace QuickStartPylon
{
    class Program
    {
        private static DataSiftClient _client;
        private static string _service = "facebook";

        static void Main(string[] args)
        {
            // Create a new DataSift client
            _client = new DataSiftClient("YOUR_USERNAME", "YOUR_IDENTITY_API_KEY");

            var recordingId = StartRecording();
            Thread.Sleep(10000);
            Analyze(recordingId);

            // Wait for key press before ending example
            Console.WriteLine("-- Press any key to exit --");
            Console.ReadKey(true);
        }

        static string StartRecording()
        {
            // Compile a filter to receive a hash
            var csdl = @"( fb.content contains_any ""wedding,engaged,engagement,marriage"" 
		                or fb.topics.name in ""Wedding,Marriage"" ) 
		                OR ( fb.parent.content contains_any ""wedding,engaged,engagement,marriage"" 
		                or fb.parent.topics.name in ""Wedding,Marriage"" )";

            var compiled = _client.Pylon.Compile(_service, csdl);
            var hash = compiled.Data.hash;
            
            Console.WriteLine("Hash: " + hash);

            // Start the recording, 
            string recordingId = null;
            var start = _client.Pylon.Start(_service, hash, "Pylon Test Filter");
            recordingId = start.Data.id;
            
            Console.WriteLine("Recording running!");
            Console.WriteLine("Recording ID: " + recordingId);
            
            // Return ID for analysis later
            return recordingId;
        }

        static void Analyze(string recordingId)
        {
            var analysisParams = new
            {
                analysis_type = "freqDist",
                parameters = new
                {
                    threshold = 5,
                    target = "fb.author.age"
                }
            };

            var analysis = _client.Pylon.Analyze(_service, recordingId, analysisParams);
            Console.WriteLine("\nAnalysis result: " + JsonConvert.SerializeObject(analysis.Data));

            var analysisWithFilter = _client.Pylon.Analyze(_service, recordingId, analysisParams, filter: "fb.author.gender == \"female\" OR fb.parent.author.gender == \"female\"");
            Console.WriteLine("\nAnalysis (with filter) result: " + JsonConvert.SerializeObject(analysisWithFilter.Data));
        }

    }
}
