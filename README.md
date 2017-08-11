# DataSift .NET Client Library

This is the official .NET library for accessing Datasift.

Please log any library issues inside this GitHub repository.

## Getting Started

To get started choose one of our quick start guides:

* [STREAM Quick Start](http://dev.datasift.com/docs/products/stream/quick-start/getting-started-net)
* [PYLON for Facebook Topic Data Quick Start](http://dev.datasift.com/docs/products/pylon-fbtd/get-started/getting-started-net)

## Installation

### 1) Sign-up to DataSift

You can sign up to DataSift for free at [http://datasift.com](http://datasift.com).

Once you've registered you can find your username and API key on your [Dashboard](http://datasift.com/dashboard).

### 2) Reference DataSift library

The easiest way to use this library is via [Nuget](https://www.nuget.org/packages/Datasift.net).

You can install the package using the Package Manager Console in Visual Studio.

```
    Install-Package Datasift.net 
```

Of course you could also download this source code and reference the DataSift project in your solution.

## Usage: REST API Calls

```c#
    var client = new DataSiftClient("YOUR_USERNAME", "YOUR_APIKEY");
    var compiled = client.Compile("interaction.content contains \"music\"");
    Console.WriteLine("Compiled to {0}, DPU = {1}", compiled.Data.hash, compiled.Data.dpu);
```

See the **DataSiftExamples** project for some simple example usage.

## Usage: Streaming Data

```c#
    class Program
    {
        private static DataSiftClient _client = null;
        private static DataSift.Streaming.DataSiftStream _stream = null;

        static void Main(string[] args)
        {
            // Create a new client
            _client = new DataSiftClient("YOUR_USERNAME", "YOUR_APIKEY");

            // Declare event handlers
            _stream.OnConnect += stream_OnConnect;
            _stream.OnMessage += stream_OnMessage;
            _stream.OnDataSiftMessage += stream_OnDataSiftMessage;

            // Connect
            _stream = _client.Connect();
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
    }
```

See the **DataSiftExamples** project for some simple example usage.

## Requirements

This library has been tested with the following frameworks:

* .NET Framework 4.5

## License

All code contained in this repository is Copyright MediaSift Ltd.

This code is released under the BSD license. Please see the LICENSE file for more details.


## Change Log

* 1.5.2 - Updated to v105 of RestSharp dependency
* 1.5.1 - Added support for the Reference Data API.
* 1.5.0 - Moved library to use v1.5 of the DataSift API, including support for Media Strategies API. 
* 1.4.0 - Moved library to use v1.4 of the DataSift API, including task endpoints. 
* 1.3.2 - Added 'analyze_queries' limit for identities.
* 1.3.1 - Fixed format validation for PYLON recording ids.
* 1.3.0 - Moved library to use v1.3 of the DataSift API, including pylon/update endpoint.
* 1.2.4 - Added support for /pylon/sample endpoint
* 1.2.3 - Added support for /account/usage endpoint
* 1.2.2 - Changes to allow inheritance of client
* 1.2.1 - Added support for ODP ingestion endpoint
* 1.2.0 - Moved library to use v1.2 of the DataSift API
* 1.1.2 - Fixed Nuget issue
* 1.1.1 - PYLON GA Release
* 1.1.0 - Added Pylon endpoints
* 1.0.4 - Fixed Code Contract packaging issue (another!)
* 1.0.3 - Fixed Code Contract packaging issue
* 1.0.2 - Fixed issue 17 - Fixed unsubscribing from a stream
* 1.0.0 - Completed rewrite of library
