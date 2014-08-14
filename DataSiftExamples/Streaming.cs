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
    static class Streaming
    {
        private static DataSiftClient _client = null;
        private static DataSift.Streaming.DataSiftStream _stream = null;

        internal static void Run(string username, string apikey)
        {
            _client = new DataSiftClient(username, apikey);

            Console.WriteLine("Running 'Streaming' example...");

            _stream = _client.Connect();
            _stream.OnConnect += stream_OnConnect;
            _stream.OnMessage += stream_OnMessage;
            _stream.OnDataSiftMessage += stream_OnDataSiftMessage;
            _stream.OnSubscribed += stream_OnSubscribed;
            _stream.OnError += stream_OnError;
            _stream.OnClosed += stream_OnClosed;
            _stream.OnDelete += stream_OnDelete;

        }

        static void stream_OnConnect()
        {
            Console.WriteLine("Connected.");

            // Compile and subscribe to a stream
            var compiled = _client.Compile("interaction.content contains \"football\"");
            Console.WriteLine("Compiled stream to {0}, DPU = {1}", compiled.Data.hash, compiled.Data.dpu);
            _stream.Subscribe(compiled.Data.hash);
        }


        static void stream_OnMessage(string hash, dynamic message)
        {
            Console.WriteLine("New interaction received on stream " + hash);
            Console.WriteLine(JsonConvert.SerializeObject(message) + "\n");
        }

        static void stream_OnDelete(string hash, dynamic message)
        {
            // You must delete the interaction to stay compliant
            Console.WriteLine("Deleted: {0}", message.interaction.id);
        }

        static void stream_OnDataSiftMessage(DataSift.Enum.DataSiftMessageStatus status, string message)
        {
            switch (status)
            {
                case DataSiftMessageStatus.Warning:
                    Console.WriteLine("WARNING: " + message);
                    break;
                case DataSiftMessageStatus.Failure:
                    Console.WriteLine("FAILURE: " + message);
                    break;
                case DataSiftMessageStatus.Success:
                    Console.WriteLine("SUCCESS: " + message);
                    break;
            }
        }

        static void stream_OnSubscribed(string hash)
        {
            Console.WriteLine("Subscribed to stream: " + hash);
        }

        static void stream_OnError(DataSift.Streaming.StreamAPIException ex)
        {
            Console.WriteLine("An error has occurred on the connection: " + ex.StackTrace);
        }

        static void stream_OnClosed()
        {
            Console.WriteLine("Connection has been closed.");
        }
    }
}
