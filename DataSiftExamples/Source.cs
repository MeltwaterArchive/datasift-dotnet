using DataSift;
using DataSift.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSiftExamples
{
    static class Source
    {
        internal static void Run(string username, string apikey)
        {
            // TODO: Insert auth tokens here from https://datasift.com/source/managed/new/facebook_page
            var authtoken = "";
            var authTokenToAddLater = "";

            if(String.IsNullOrEmpty(authtoken) || (String.IsNullOrEmpty(authTokenToAddLater)))
                throw new ArgumentException("Set the authtoken & authTokenToAddLater variables to a valid tokens");

            var client = new DataSiftClient(username, apikey);

            Console.WriteLine("Running 'Source' example...");

            var get = client.Source.Get(page: 1, perPage: 5);
            Console.WriteLine("\nList of sources: " + JsonConvert.SerializeObject(get.Data));

            var prms = new
            {
                likes = true,
                posts_by_others = true,
                comments = true,
                page_likes = false
            };

            var resources = new[] {
                    new { 
                        parameters = new {
                            url = "http://www.facebook.com/theguardian",
                            title = "The Guardian",
                            id = 10513336322
                        }
                    }
                };

            var resourcesToAddLater = new[] {
                    new { 
                        parameters = new {
                            url = "http://www.facebook.com/thesun",
                            title = "The Sun",
                            id = 161385360554578
                        }
                    }
                };

            var auth = new[] {
                    new { 
                        parameters = new {
                            value = authtoken
                        }
                    }
                };

            var authToAddLater = new[] {
                    new { 
                        parameters = new {
                            value = authTokenToAddLater
                        }
                    }
                };

            var create = client.Source.Create("facebook_page", "Example source", prms, resources, auth);
            Console.WriteLine("\nCreated source: {0}", create.Data.id);

            client.Source.Start(create.Data.id);
            Console.WriteLine("\nStarted source.");

            var update = client.Source.Update(create.Data.id, name: "Updated example source");
            Console.WriteLine("\nUpdated source: {0}", update.Data.id);

            var getSource = client.Source.Get(id: create.Data.id);
            Console.WriteLine("\nSource details: " + JsonConvert.SerializeObject(getSource.Data));

            var addResource = client.Source.ResourceAdd(create.Data.id, resourcesToAddLater);
            Console.WriteLine("\nAdded resource ID: " + addResource.Data.resources[1].resource_id);

            var removeResource = client.Source.ResourceRemove(create.Data.id, new string[] { addResource.Data.resources[1].resource_id });
            Console.WriteLine("\nRemoved resource ID: " + addResource.Data.resources[1].resource_id);

            var addAuth = client.Source.AuthAdd(create.Data.id, authToAddLater);
            Console.WriteLine("\nAdded auth ID: " + addAuth.Data.auth[1].identity_id);

            var removeAuth = client.Source.ResourceRemove(create.Data.id, new string[] {  addAuth.Data.auth[1].identity_id });
            Console.WriteLine("\nRemoved auth ID: " + addAuth.Data.auth[1].identity_id);

            client.Source.Stop(create.Data.id);
            Console.WriteLine("\nStopped source.");

            var log = client.Source.Log(create.Data.id, 1, 5);
            Console.WriteLine("\nSource log: " + JsonConvert.SerializeObject(log.Data.log_entries));

            client.Source.Delete(create.Data.id);
            Console.WriteLine("\nDeleted source.");
        }
    }
}
