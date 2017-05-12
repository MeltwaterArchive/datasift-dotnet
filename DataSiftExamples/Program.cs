using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSiftExamples
{
    class Program
    {
        // TODO: Insert your API credentials here, you can find them on your dashboard - https://datasift.com/dashboard
        const string username = "";
        const string apikey = "";

        static void Main(string[] args)
        {
            // Ask user for example to run
            PrintIntro();

            WaitForExampleChoice();

            // Wait for key press before ending example
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);

        }

        static void WaitForExampleChoice()
        {
            var command = Console.ReadKey(true);

            switch (command.Key)
            {
                case ConsoleKey.D0:
                    Streaming.Run(username, apikey);
                    break;

                case ConsoleKey.D1:
                    Core.Run(username, apikey);
                    break;

                case ConsoleKey.D2:
                    Push.Run(username, apikey);
                    break;

                case ConsoleKey.D3:
                    HistoricsPreview.Run(username, apikey);
                    break;

                case ConsoleKey.D4:
                    Historics.Run(username, apikey);
                    break;

                case ConsoleKey.D5:
                    Source.Run(username, apikey);
                    break;

                case ConsoleKey.D6:
                    Pylon.Run(username, apikey);
                    break;

                case ConsoleKey.D7:
                    AccountIdentity.Run(username, apikey);
                    break;

                case ConsoleKey.D8:
                    Ingestion.Run(username, apikey);
                    break;

                case ConsoleKey.D9:
                    Account.Run(username, apikey);
                    break;
                    
                case ConsoleKey.A:
                    AnalysisTask.Run(username, apikey);
                    break;

                case ConsoleKey.B:
                    StrategyTask.Run(username, apikey);
                    break;

                default:
                    Console.WriteLine("Unknown example, please try again.\n");
                    WaitForExampleChoice();
                    break;
            }
        }

        static void PrintIntro()
        {
            Console.WriteLine("Choose one of the examples to run:\n");
            Console.WriteLine("    0. Streaming");
            Console.WriteLine("    1. Core API endpoints");
            Console.WriteLine("    2. Push API endpoints");
            Console.WriteLine("    3. Historics preview API endpoints");
            Console.WriteLine("    4. Historics API endpoints");
            Console.WriteLine("    5. Source API endpoints");
            Console.WriteLine("    6. Pylon API endpoints");
            Console.WriteLine("    7. Account Identity API endpoints");
            Console.WriteLine("    8. Ingest data with ODP");
            Console.WriteLine("    9. Account usage API endpoint");
            Console.WriteLine("    A. Analysis tasks example");
            Console.WriteLine("    B. Insight tasks example (Media Strategies API)");
            Console.WriteLine("\nPress a key to continue...\n");
        }
    }
}
