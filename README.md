# DataSift .NET Client Library - BETA

This is the official .NET library for accessing Datasift.

Please log any library issues inside this GitHub repository.

## Installation

### 1) Sign-up to DataSift

You can sign up to DataSift for free at [http://datasift.com](http://datasift.com).

Once you've registered you can find your username and API key on your [Dashboard](http://datasift.com/dashboard).

### 2) Reference DataSift library

Soon the library will be on NuGet. In the meantime you will need to reference the 'DataSift' library within this solution in your project.

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

1.0.0 - Completed rewrite of library
1.0.2 - Fixed issue 17 - Fixed unsubscribing from a stream