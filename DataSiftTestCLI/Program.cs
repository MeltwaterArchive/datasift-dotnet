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
                case "perpare":
                    break;
                case "start":
                    break;
                case "stop":
                    break;
                case "status":
                    break;
                case "update":
                    break;
                case "delete":
                    break;
                case "get":
                    response = client.Historics.Get(id: _argsParser.GetParameter<string>("id"), max: _argsParser.GetParameter<int?>("max"), 
                        page: _argsParser.GetParameter<int?>("page"), withEstimate: _argsParser.GetParameter<bool?>("with_estimate"));
                    break;
                default:
                    throw new ApplicationException("Unrecognised command: " + command);
            }

//            def run_historics_command (c, command, p)
//  case command
//    when 'prepare'
//      c.historics.prepare(p['hash'], p['start'], p['end'], p['name'], opt(p['sources'], 'twitter'), opt(p['sample'], 10))
//    when 'start'
//      c.historics.start(p['id'])
//    when 'stop'
//      c.historics.stop(p['id'], opt(p['reason'], ''))
//    when 'status'
//      c.historics.status(p['start'], p['end'], opt(p['sources'], 'twitter'))
//    when 'update'
//      c.historics.update(p['id'], p['name'])
//    when 'delete'
//      c.historics.delete(p['id'])
//    when 'get'
//      c.historics.get(opt(p['max'], 20), opt(p['page'], 1), opt(p['with_estimate'], 1))
//    else
//      err 'Unknown command for the historics endpoint'
//      exit
//  end
//end

            return response;
        }

    }
}
