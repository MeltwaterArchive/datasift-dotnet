using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataSift.Rest;
using System.Dynamic;
using Newtonsoft.Json.Converters;

namespace DataSiftTestCLI
{
    class CmdLineArgs
    {
        Dictionary<string, dynamic> _args;

        internal bool ParseAndValidate(string[] args)
        {
            var values = new List<string>();
            var groups = new List<List<string>>();
            List<string> currList = null;

            foreach (var arg in args)
            {
                if (IsOption(arg))
                {
                    if (currList != null)
                        groups.Add(currList);

                    currList = new List<string>();
                    currList.Add(Regex.Replace(arg, "-", ""));
                }
                else
                {
                    if (currList != null)
                        currList.Add(arg);
                }
            }

            if (currList != null)
                groups.Add(currList);

            _args = ProcessGroups(groups);

            Console.WriteLine(JsonConvert.SerializeObject(args));
            Console.WriteLine(JsonConvert.SerializeObject(_args));

            var argErrors = ValidateArguments();

            if (argErrors.Count() > 0)
            {
                PrintHelp(argErrors);
            }

            return argErrors.Count() == 0;
        }

        internal void PrintHelp(List<string> errors = null)
        {
            if (errors != null)
            {
                Console.WriteLine("Invalid parameters: ");

                foreach (var error in errors)
                {
                    Console.WriteLine("\t" + error);
                }
            }

            Console.WriteLine(Environment.NewLine + "Usage: DataSiftTestCLI.exe -a [-e] -c [-u] [-p*]");
            Console.WriteLine("\t-a : Authentication details in format 'username:apikey'");
            Console.WriteLine("\t-e : The API endpoint, e.g. core");
            Console.WriteLine("\t-c : The command you want to perform, e.g. validate");
            Console.WriteLine("\t-u : The API domain to hit, e.g. api.datasift.com");
            Console.WriteLine("\t-p [name] [value] : Additional parameters for the command" + Environment.NewLine);
        }

        internal List<string> ValidateArguments()
        {
            var errors = new List<string>();

            // Check auth
            if (!_args.ContainsKey("a"))
            {
                errors.Add("-a : No authentication details provided");
            }
            else
            {
                // Check auth format
                var authDetails = _args["a"];

                if (!authDetails.GetType().IsArray)
                    errors.Add("-a : Authentication details must be in format 'username apikey'");
                else
                {
                    if (((string[])authDetails).Length != 2)
                        errors.Add("-a : Authentication details must be in format 'username apikey'");
                }
            }

            // Check command
            if (!_args.ContainsKey("c"))
            {
                errors.Add("-c : No command specified");
            }
            else
            {
                if (_args["c"].GetType().IsArray)
                    errors.Add("-c : Command details must be a single string");
            }

            // Check param format
            if (_args.ContainsKey("p"))
            {
                var psValid = true;
                var p = _args["p"];

                // One parameter
                if (p.GetType().IsArray)
                {
                    if (((string[])p).Length != 2)
                    {
                        // Not 2 components to value, should be two
                        psValid = false;
                    }
                }
                // Multiple parameters
                else if (p.GetType() == typeof(List<dynamic>))
                {
                    foreach (var pItem in (List<dynamic>)p)
                    {
                        if (pItem.GetType().IsArray)
                        {
                            if (((string[])pItem).Length != 2)
                            {
                                // Not 2 components to value, should be two
                                psValid = false;
                            }
                        }
                        else
                            psValid = false;

                    }
                }
                // Is one value, so must be invalid
                else
                {
                    psValid = false;
                }

                if (!psValid)
                {
                    errors.Add("-p : All parameters must be in format 'name value'");
                }
            }

            return errors;
        }

        private Dictionary<string, dynamic> ProcessGroups(List<List<string>> groups)
        {
            var result = new Dictionary<string, dynamic>();

            foreach (var group in groups)
            {
                var opt = group[0];
                var values = group.Skip(1).ToArray();

                if (!result.ContainsKey(opt))
                {
                    if (values.Count() == 1)
                        result.Add(opt, values[0]);
                    else if (values.Count() > 1)
                        result.Add(opt, values);
                }
                else
                {
                    var current = result[opt];

                    dynamic newVal = null;

                    if (values.Count() == 1)
                        newVal = values[0];
                    else if (values.Count() > 1)
                        newVal = values;

                    if (current.GetType() == typeof(List<dynamic>))
                    {
                        current.Add(newVal);
                    }
                    else
                    {
                        var list = new List<dynamic>();
                        list.Add(current);
                        list.Add(newVal);
                        result[opt] = list;
                    }

                }
            }

            return result;
        }

        private bool IsOption(string arg)
        {
            var optTest = new Regex(@"^(-[a-zA-Z]$)|(--\w+$)");
            return optTest.IsMatch(arg);
        }

        internal T Get<T>(string argName)
        {
            if(_args.ContainsKey(argName))
            {
                return (T)_args[argName];
            }

            return default(T);
        }

        internal bool Contains(string argName)
        {
            return _args.ContainsKey(argName);
        }

        internal T GetParameter<T>(string paramName)
        {
            string sValue = null;

            if (_args.ContainsKey("p"))
            {
                var parameters = _args["p"];

                // One parameter
                if (parameters.GetType().IsArray)
                {
                    if(((string[])parameters)[0] == paramName)
                    {
                        sValue = ((string[])parameters)[1];
                    }
                }
                // Multiple parameters
                else if (parameters.GetType() == typeof(List<dynamic>))
                {
                    foreach (var pItem in (List<dynamic>)parameters)
                    {
                        if (pItem[0] == paramName)
                        {
                            sValue = pItem[1];
                        }
                    }
                }
            }
            
            if(sValue != null)
            {
                dynamic val = null;
                Type type = typeof(T);

                if (type == typeof(string))
                {
                    val = sValue;
                }
                else if (type == typeof(Nullable<Int32>))
                {
                    int output;
                    int? parsed = null;

                    if (Int32.TryParse(sValue, out output))
                        parsed = output;

                    val = parsed;
                }
                else if (type == typeof(int))
                {
                    val = int.Parse(sValue);
                }
                else if (type == typeof(Nullable<bool>))
                {
                    bool output;
                    if (bool.TryParse(sValue, out output)) val = output;
                }
                else if (type == typeof(bool))
                {
                    val = bool.Parse(sValue);
                }
                else if (type == typeof(DateTimeOffset))
                {
                    double epoch = double.Parse(sValue);
                    val = UnixTimeStampToDateTimeOffset(epoch);
                }
                else if (type == typeof(string[]))
                {
                    val = sValue.Split(',');
                }
                else if (type == typeof(List<HistoricsPreviewParameter>))
                {
                    var prevParams = sValue.Split(';');
                    var parsedParams = new List<HistoricsPreviewParameter>();

                    foreach (var prevP in prevParams)
                    {
                        var detail = prevP.Split(',');

                        parsedParams.Add(new HistoricsPreviewParameter() { 
                            Target = detail[0],
                            Analysis = detail[1],
                            Argument = detail[2]
                        });
                    }

                    val = parsedParams;
                }
                else if (type == typeof(ExpandoObject))
                {
                    var converter = new ExpandoObjectConverter();
                    val = JsonConvert.DeserializeObject<ExpandoObject>(sValue, converter);
                }
                else if (type == typeof(List<ExpandoObject>))
                {   
                    var converter = new ExpandoObjectConverter();
                    val = JsonConvert.DeserializeObject<List<ExpandoObject>>(sValue, converter);
                }

                var t = typeof(T);

                if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    if (val == null)
                    {
                        return default(T);
                    }

                    t = Nullable.GetUnderlyingType(t);
                }

                return (T)Convert.ChangeType(val, t);
            }

            return default(T);
        }

        internal DateTimeOffset UnixTimeStampToDateTimeOffset(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTimeOffset dtDateTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }
    }

}
