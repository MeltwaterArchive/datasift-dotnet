﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using System.ComponentModel;


namespace DataSift.Rest
{
    internal class RestAPIRequest : IRestAPIRequest
    {
        private RestClient _client;

        internal RestAPIRequest(string username, string apikey)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            _client = new RestClient("https://api.datasift.com/v1");
            _client.Authenticator = new HttpBasicAuthenticator(username, apikey);

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            _client.UserAgent = "DataSift/v1 Dotnet/v" + version.ToString();
        }

        public RestAPIResponse Request(string endpoint, dynamic parameters = null, RestSharp.Method method = Method.GET)
        {
            var request = new RestRequest(endpoint, method);
            RestAPIResponse result = null;

            if (parameters != null) request.Parameters.AddRange(APIHelpers.ParseParameters(endpoint, parameters));

            IRestResponse response = _client.Execute(request);

            if(endpoint == "pull")
            {
                result = new PullAPIResponse() { RateLimit = APIHelpers.ParseRateLimitHeaders(response.Headers), StatusCode = response.StatusCode, PullDetails = APIHelpers.ParsePullDetailHeaders(response.Headers) };
                result.Data = APIHelpers.DeserializeResponse(response.Content, ((PullAPIResponse)result).PullDetails.Format);
            }
            else
            {
                result = new RestAPIResponse() { RateLimit = APIHelpers.ParseRateLimitHeaders(response.Headers), StatusCode = response.StatusCode };
                result.Data = APIHelpers.DeserializeResponse(response.Content);
            }
            
            switch((int)response.StatusCode)
            {
                // Ok status codes
                case 200:
                case 201:
                case 202:
                case 204:
                    break;
                
                //Error status codes
                case 400:
                case 401:
                case 403:
                case 404:
                case 405:
                case 409:
                case 413:
                case 416:
                case 500:
                case 503:
                     throw new RestAPIException(result, (APIHelpers.HasAttr(result.Data, "error")) ? result.Data.error : "The request failed, please see the Data & StatusCode properties for more details.");
            }
            
            return result;

        }
    }

}
