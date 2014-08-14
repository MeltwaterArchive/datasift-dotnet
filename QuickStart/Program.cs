using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataSift;
using DataSift.Enum;
using DataSift.Streaming;

namespace QuickStart
{
    class Program
    {
        // References we'll need to keep
        private static DataSiftStream _stream;
        private static string _hash;

        static void Main(string[] args)
        {
            // Create a new DataSift client
            var client = new DataSiftClient("DATASIFT_USERNAME", "DATASIFT_APIKEY");

            // Compile filter
            var csdl = @"tag.source ""Pandora"" { links.domain == ""pandora.com"" }
                        tag.source ""SoundCloud"" { links.domain == ""soundcloud.com"" }
                        tag.source ""Spotify"" { links.domain == ""spotify.com"" }

                        return {
	                        links.domain in ""pandora.com,soundcloud.com,spotify.com""
                            AND interaction.type == ""twitter""
                        }";

            var compiled = client.Compile(csdl);
            _hash = compiled.Data.hash;

            _stream = client.Connect();
            _stream.OnConnect += stream_OnConnect;
            _stream.OnMessage += stream_OnMessage;
            _stream.OnDelete += stream_OnDelete;
            _stream.OnDataSiftMessage += stream_OnDataSiftMessage;
            _stream.OnClosed += stream_OnClosed;

            // Wait for key press before ending example
            Console.WriteLine("-- Press any key to exit --");
            Console.ReadKey(true);

        }

        static void stream_OnConnect()
        {
            Console.WriteLine("Connected to DataSift.");

            // Subscribe to stream
            _stream.Subscribe(_hash);
        }

        static void stream_OnMessage(string hash, dynamic message)
        {
            Console.WriteLine("{0}: {1}", message.interaction.tag_tree.source[0], message.interaction.content);
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

        static void stream_OnClosed()
        {
            Console.WriteLine("Connection has been closed.");
        }
    }
}
