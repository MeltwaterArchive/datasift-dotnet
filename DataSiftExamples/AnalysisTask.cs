using DataSift;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace DataSiftExamples
{
    static class AnalysisTask
    {
        // Note that to run the PYLON example you must use an API key which corresponds to a valid identity with Facebook access
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            // TODO: Insert the service, recording id and analysis parameters you'd like to use
            var service = "";
            var recordingId = "";

            dynamic parameters = new {
                        parameters = new
                        {
                            analysis_type = "timeSeries",
                            parameters = new
                            {
                                interval = "hour",
                                span = 1
                            }
                        }
                    };

            Console.WriteLine("Running 'Analysis tasks' example...");

            var get = client.Pylon.Task.Get(service, "analysis");
            Console.WriteLine("\nCurrent list of tasks: " + JsonConvert.SerializeObject(get.Data));

            var create = client.Pylon.Task.Create(service, recordingId, "New analysis task", "analysis", parameters);
            Console.WriteLine("\nCreated task: " + JsonConvert.SerializeObject(create.Data));

            var getOne = client.Pylon.Task.Get(service, "analysis", taskId: create.Data.id);
            Console.WriteLine("\nGot task: " + JsonConvert.SerializeObject(getOne.Data));

            while(getOne.Data.status != "completed")
            {
                Thread.Sleep(2000); // Wait for 2 seconds before checking again
                getOne = client.Pylon.Task.Get(service, "analysis", taskId: create.Data.id);
                Console.WriteLine("\nStatus: " + getOne.Data.status);
            }

            Console.WriteLine("\nResult: " + JsonConvert.SerializeObject(getOne.Data));

        }
    }
}
