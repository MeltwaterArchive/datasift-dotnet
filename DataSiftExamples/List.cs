using DataSift;
using DataSift.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSiftExamples
{
    static class List
    {
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            Console.WriteLine("Running 'List' example...");

            var get = client.List.Get();
            Console.WriteLine("\nCurrent of dynamic lists: " + JsonConvert.SerializeObject(get.Data));

            var create = client.List.Create(ListType.Integer, "Example integer list");
            Console.WriteLine("\nCreated list: {0}", create.Data.id);

            client.List.Add(create.Data.id, new int[] { 1, 2, 3, 4, 5 });
            Console.WriteLine("\nAdded items to list");

            client.List.Remove(create.Data.id, new int[] { 1, 2 });
            Console.WriteLine("\nRemoved items from list");

            var exists = client.List.Exists(create.Data.id, new int[] { 2, 4 });
            Console.WriteLine("\nExistance in list: " + JsonConvert.SerializeObject(exists.Data));

            var replace = client.List.ReplaceStart(create.Data.id);
            Console.WriteLine("\nReplace started with ID: " + replace.Data.id);

            client.List.ReplaceAdd(replace.Data.id, new int[] { 6, 7, 8, 9, 10 });
            Console.WriteLine("\nAdded new items in replace.");

            client.List.ReplaceCommit(replace.Data.id);
            Console.WriteLine("\nCommitted bulk replace");

            client.List.Delete(create.Data.id);
            Console.WriteLine("\nDeleted list.");
        }
    }
}
