using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using DataSift;
using DataSift.Rest;
using DataSift.Enum;
using System.Dynamic;

namespace DataSiftTestCLI
{
    class Program
    {
        static CmdLineArgs _argsParser = new CmdLineArgs();

        static void Main(string[] args)
        {
            if(_argsParser.ParseAndValidate(args))
                RunCommand();
        }

        private static void RunCommand()
        {
            string endpoint = "core";
            string[] auth = _argsParser.Get<string[]>("a");
            DataSiftClient datasift= null;

            if(_argsParser.Contains("u"))
                datasift = new DataSiftClient(auth[0], auth[1], baseUrl: _argsParser.Get<string>("u"));
            else
                datasift = new DataSiftClient(auth[0], auth[1]);

            if (_argsParser.Contains("e"))
                endpoint = _argsParser.Get<string>("e");

            var command = _argsParser.Get<string>("c").ToLower();

            RestAPIResponse response = null;

            switch(endpoint.ToLower())
            {
                case "core":
                    response = Core(datasift, command);
                    break;
                case "historics":
                    response = Historics(datasift, command);
                    break;
                case "preview":
                    response = HistoricsPreview(datasift, command);
                    break;
                case "source":
                    response = Source(datasift, command);
                    break;
                case "push":
                    response = Push(datasift, command);
                    break;
                case "pylon":
                    response = Pylon(datasift, command);
                    break;
            }

            WriteResult(response);

        }

        private static void WriteResult(RestAPIResponse result)
        {
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        private static RestAPIResponse Core(DataSiftClient client, string command)
        {
            RestAPIResponse response = null;

            switch(command)
            {
                case "validate":
                    response = client.Validate(_argsParser.GetParameter<string>("csdl"));
                    break;
                case "compile":
                    response = client.Compile(_argsParser.GetParameter<string>("csdl"));
                    break;
                case "usage":
                    UsagePeriod period;

                    if (Enum.TryParse(_argsParser.GetParameter<string>("period"), out period))
                        client.Usage(period);
                    else
                        client.Usage();

                    break;
                case "balance":
                    response = client.Balance();
                    break;
                case "dpu":
                    response = client.Compile(_argsParser.GetParameter<string>("hash"));
                    break;
                default:
                    throw new ApplicationException("Unrecognised command: " + command);
            }

            return response;
        }

        private static RestAPIResponse Historics(DataSiftClient client, string command)
        {
            RestAPIResponse response = null;

            switch (command)
            {
                case "prepare":
                    
                    Sample sampleTemp;
                    Sample? sample = null;

                    if (Enum.TryParse(_argsParser.GetParameter<string>("sample"), out sampleTemp))
                        sample = sampleTemp;
                    
                    response = client.Historics.Prepare(_argsParser.GetParameter<string>("hash"), _argsParser.GetParameter<DateTimeOffset>("start"),
                            _argsParser.GetParameter<DateTimeOffset>("end"), _argsParser.GetParameter<string>("name"), _argsParser.GetParameter<string[]>("sources"),
                            sample);
                    break;
                case "start":
                    response = client.Historics.Start(_argsParser.GetParameter<string>("id"));
                    break;
                case "stop":
                    response = client.Historics.Stop(_argsParser.GetParameter<string>("id"));
                    break;
                case "status":
                    response = client.Historics.Status(_argsParser.GetParameter<DateTimeOffset>("start"), _argsParser.GetParameter<DateTimeOffset>("stop"), _argsParser.GetParameter<string[]>("sources"));
                    break;
                case "update":
                    response = client.Historics.Update(_argsParser.GetParameter<string>("id"), _argsParser.GetParameter<string>("name"));
                    break;
                case "delete":
                    response = client.Historics.Delete(_argsParser.GetParameter<string>("id"));
                    break;
                case "get":
                    response = client.Historics.Get(id: _argsParser.GetParameter<string>("id"), max: _argsParser.GetParameter<int?>("max"), 
                        page: _argsParser.GetParameter<int?>("page"), withEstimate: _argsParser.GetParameter<bool?>("with_estimate"));
                    break;
                default:
                    throw new ApplicationException("Unrecognised command: " + command);
            }

            return response;
        }

        private static RestAPIResponse HistoricsPreview(DataSiftClient client, string command)
        {
            RestAPIResponse response = null;

            switch (command)
            {
                case "create":
                    response = client.HistoricsPreview.Create(_argsParser.GetParameter<string>("hash"),
                        _argsParser.GetParameter<string[]>("sources"), _argsParser.GetParameter<List<HistoricsPreviewParameter>>("parameters"),
                        _argsParser.GetParameter<DateTimeOffset>("start"), _argsParser.GetParameter<DateTimeOffset?>("end"));
                    break;
                case "get":
                    response = client.HistoricsPreview.Get(_argsParser.GetParameter<string>("id"));
                    break;
                default:
                    throw new ApplicationException("Unrecognised command: " + command);
            }

            return response;
   
        }

        private static RestAPIResponse Source(DataSiftClient client, string command)
        {
            RestAPIResponse response = null;

            switch (command)
            {
                case "create":
                    response = client.Source.Create(_argsParser.GetParameter<string>("source_type"),
                        _argsParser.GetParameter<string>("name"), _argsParser.GetParameter<ExpandoObject>("parameters"),
                        _argsParser.GetParameter<List<ExpandoObject>>("resources"), _argsParser.GetParameter<List<ExpandoObject>>("auth"));
                    break;
                case "update":
                    response = client.Source.Update(_argsParser.GetParameter<string>("id"), _argsParser.GetParameter<string>("source_type"),
                        _argsParser.GetParameter<string>("name"), _argsParser.GetParameter<ExpandoObject>("parameters"),
                        _argsParser.GetParameter<List<ExpandoObject>>("resources"), _argsParser.GetParameter<List<ExpandoObject>>("auth"));
                    break;
                case "delete":
                    response = client.Source.Delete(_argsParser.GetParameter<string>("id"));
                    break;
                case "start":
                    response = client.Source.Start(_argsParser.GetParameter<string>("id"));
                    break;
                case "stop":
                    response = client.Source.Stop(_argsParser.GetParameter<string>("id"));
                    break;
                case "log":
                    response = client.Source.Log(_argsParser.GetParameter<string>("id"), _argsParser.GetParameter<int?>("page"),
                        _argsParser.GetParameter<int?>("per_page"));
                    break;
                case "get":
                    response = client.Source.Get(_argsParser.GetParameter<string>("id"), 
                        _argsParser.GetParameter<int?>("page"),
                        _argsParser.GetParameter<int?>("per_page"), _argsParser.GetParameter<string>("id"));
                    break;
                default:
                    throw new ApplicationException("Unrecognised command: " + command);
            }

            return response;
        }

        private static RestAPIResponse Push(DataSiftClient client, string command)
        {
            RestAPIResponse response = null;

            OrderDirection tmpOrderDir;
            OrderDirection? orderDir = null;
            OrderBy tmpOrderBy;
            OrderBy? orderBy = null;

            if (Enum.TryParse(_argsParser.GetParameter<string>("order_dir"), out tmpOrderDir))
                orderDir = tmpOrderDir;

            if (Enum.TryParse(_argsParser.GetParameter<string>("order_by"), out tmpOrderBy))
                orderBy = tmpOrderBy;

            switch (command)
            {
                case "validate":
                    response = client.Push.Validate(_argsParser.GetParameter<string>("output_type"),
                        _argsParser.GetParameter<ExpandoObject>("output_params"));
                    break;
                case "create":
                    PushStatus statusTemp;
                    PushStatus? status = null;

                    if (Enum.TryParse(_argsParser.GetParameter<string>("initial_status"), out statusTemp))
                        status = statusTemp;

                    response = client.Push.Create(_argsParser.GetParameter<string>("name"), _argsParser.GetParameter<string>("output_type"),
                        _argsParser.GetParameter<ExpandoObject>("output_params"), hash: _argsParser.GetParameter<string>("hash"), historicsId: _argsParser.GetParameter<string>("historics_id"),
                        initialStatus: status, start: _argsParser.GetParameter<DateTimeOffset?>("start"), end: _argsParser.GetParameter<DateTimeOffset?>("end"));
                    break;
                case "update":
                    if(_argsParser.Contains("output_params"))
                        response = client.Push.Update(_argsParser.GetParameter<string>("id"), _argsParser.GetParameter<ExpandoObject>("output_params"), name: _argsParser.GetParameter<string>("name"));
                    else
                        response = client.Push.Update(_argsParser.GetParameter<string>("id"), name: _argsParser.GetParameter<string>("name"));
                    break;
                case "pause":
                    response = client.Push.Pause(_argsParser.GetParameter<string>("id"));
                    break;
                case "resume":
                    response = client.Push.Resume(_argsParser.GetParameter<string>("id"));
                    break;
                case "delete":
                    response = client.Push.Delete(_argsParser.GetParameter<string>("id"));
                    break;
                case "stop":
                    response = client.Push.Stop(_argsParser.GetParameter<string>("id"));
                    break;
                case "log":
                    response = client.Push.Log(_argsParser.GetParameter<string>("id"), _argsParser.GetParameter<int?>("page"),
                        _argsParser.GetParameter<int?>("per_page"), orderDir);

                    break;
                case "get":
                    response = client.Push.Get(id: _argsParser.GetParameter<string>("id"), hash: _argsParser.GetParameter<string>("hash"),
                        historicsId: _argsParser.GetParameter<string>("historics_id"), page: _argsParser.GetParameter<int?>("page"),
                        perPage: _argsParser.GetParameter<int?>("per_page"), orderBy: orderBy, orderDirection: orderDir, includeFinished: _argsParser.GetParameter<bool?>("include_finished"));
                    break;
                default:
                    throw new ApplicationException("Unrecognised command: " + command);
            }

            return response;
        }

        private static RestAPIResponse Pylon(DataSiftClient client, string command)
        {
            RestAPIResponse response = null;

            switch (command)
            {
                case "validate":
                    response = client.Pylon.Validate(_argsParser.GetParameter<string>("csdl"));
                    break;
                case "compile":
                    response = client.Pylon.Compile(_argsParser.GetParameter<string>("csdl"));
                    break;
                case "start":
                    response = client.Pylon.Start(_argsParser.GetParameter<string>("hash"), _argsParser.GetParameter<string>("name"));
                    break;
                case "stop":
                    response = client.Pylon.Stop(_argsParser.GetParameter<string>("hash"));
                    break;
                case "get":
                    response = client.Pylon.Get(_argsParser.GetParameter<string>("hash"));
                    break;
                case "tags":
                    response = client.Pylon.Get(_argsParser.GetParameter<string>("hash"));
                    break;
                case "analyze":
                    response = client.Pylon.Analyze(_argsParser.GetParameter<string>("hash"),
                        _argsParser.GetParameter<ExpandoObject>("parameters"), _argsParser.GetParameter<string>("filter"),
                        _argsParser.GetParameter<DateTimeOffset?>("start"), _argsParser.GetParameter<DateTimeOffset?>("end"));
                    break;
                default:
                    throw new ApplicationException("Unrecognised command: " + command);
            }

            return response;
        }

    }
}
