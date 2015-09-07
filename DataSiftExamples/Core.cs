using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSift;

namespace DataSiftExamples
{
    static class Core
    {
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            Console.WriteLine("Running 'Core' example...");

            var compiled = client.Compile("interaction.content contains \"music\"");
            Console.WriteLine("\nCompiled to {0}, DPU = {1}", compiled.Data.hash, compiled.Data.dpu);

            var usage = client.Usage();
            Console.WriteLine("\nUsage report: " + usage.ToJson());

            var dpu = client.DPU(hash: compiled.Data.hash);
            Console.WriteLine("\nDPU report: " + dpu.ToJson());

            var balance = client.Balance();
            Console.WriteLine("\nBalance report: " + balance.ToJson());

        }
    }
}
