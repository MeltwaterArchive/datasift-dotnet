using DataSift.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataSift
{
    public class APIHelpers
    {
        public static dynamic DeserializeResponse(string data, string format = null)
        {
            // Empty responses
            if (String.IsNullOrWhiteSpace(data)) return null;
            if (data.Trim() == "[]") return null;

            var converter = new ExpandoObjectConverter();
            data = data.Trim();

            if(format != null)
            {
                // Data response (such as from Pull requests)
                switch(format)
                {
                    case Constants.DATA_FORMAT_META_PLUS_INTERACTIONS:
                        return JsonConvert.DeserializeObject<ExpandoObject>(data, converter);
                    case Constants.DATA_FORMAT_ARRAY_INTERACTIONS:
                        return JsonConvert.DeserializeObject<List<ExpandoObject>>(data, converter);
                    case Constants.DATA_FORMAT_NEWLINE_INTERACTIONS:

                        var items = new List<ExpandoObject>();

                        foreach(var line in data.Split('\n'))
                        {
                            items.Add(JsonConvert.DeserializeObject<ExpandoObject>(line, converter));
                        }
                        return items;

                    default:
                        throw new ArgumentException(Messages.UNRECOGNISED_DATA_FORMAT, "format");
                }

            }
            else
            {
                // Standard API responses
                if (data.StartsWith("["))
                {
                    // Data is an array of items
                    if (data.StartsWith("[\""))
                    {
                        // Is an array of strings
                        return JsonConvert.DeserializeObject<List<string>>(data, converter);
                    }
                    else
                        return JsonConvert.DeserializeObject<List<ExpandoObject>>(data, converter);
                }
                else
                {
                    return JsonConvert.DeserializeObject<ExpandoObject>(data, converter);
                }
            }
            
        }

        public static bool HasAttr(dynamic expando, string key)
        {
            if (expando == null) return false;

            return ((IDictionary<string, object>)expando).ContainsKey(key);
        }

        public static RateLimitInfo ParseRateLimitHeaders(IList<Parameter> headers)
        {
            var result = new RateLimitInfo();

            foreach (var header in headers)
            {
                switch (header.Name)
                {
                    case Constants.HEADER_RATELIMIT_LIMIT:
                        result.Limit = int.Parse((string)header.Value);
                        break;
                    case Constants.HEADER_RATELIMIT_REMAINING:
                        result.Remaining = int.Parse((string)header.Value);
                        break;
                    case Constants.HEADER_RATELIMIT_COST:
                        result.Cost = int.Parse((string)header.Value);
                        break;
                }
            }

            return result;
        }

        public static PullInfo ParsePullDetailHeaders(IList<Parameter> headers)
        {
            var result = new PullInfo();

            foreach (var header in headers)
            {
                switch (header.Name)
                {
                    case Constants.HEADER_DATA_FORMAT:
                        result.Format = (string)header.Value;
                        break;
                    case Constants.HEADER_CURSOR_CURRENT:
                        result.CursorCurrent = (string)header.Value;
                        break;
                    case Constants.HEADER_CURSOR_NEXT:
                        result.CursorNext = (string)header.Value;
                        break;
                }
            }

            return result;
        }

        public static dynamic ParseParameters(string endpoint, dynamic parameters)
        {
            var result = new ExpandoObject();

            foreach (var prop in parameters.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var val = prop.GetValue(parameters, null);
                if (val != null)
                {
                    if (val.GetType().IsEnum)
                        val = GetEnumDescription(val);
                    else if (
                            !(endpoint.StartsWith("list/") || endpoint.StartsWith("source/"))
                            &&val.GetType().IsArray 
                            && (val.GetType().GetElementType() == typeof(string) || val.GetType().GetElementType() == typeof(int))
                        )
                        val = String.Join(",", val);
                    else if (val.GetType().IsArray)
                        val = JsonConvert.SerializeObject(val);
                    else if (val.GetType() == typeof(DateTimeOffset) || val.GetType().UnderlyingSystemType == typeof(DateTimeOffset))
                        val = ToUnixTime(val);
                    else if (val.GetType() == typeof(List<HistoricsPreviewParameter>))
                        val = String.Join<HistoricsPreviewParameter>(";", val);
                    else if ( 
                                !(endpoint.StartsWith("analysis/analyze"))
                                && (val.GetType().IsGenericType || val.GetType() == typeof(ExpandoObject))
                            )
                        val = JsonConvert.SerializeObject(val);

                    ((IDictionary<string, object>)result)[prop.Name] = val;
                }
            }

            return result;
        }

        public static string GetEnumDescription(dynamic enumerationValue)
        {
            Type type = enumerationValue.GetType();

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString().ToLower();

        }

        public static int ToUnixTime(DateTimeOffset time)
        {
            return (int)(time - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }
    }
}
