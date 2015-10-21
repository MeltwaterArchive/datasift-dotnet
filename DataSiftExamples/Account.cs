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
    static class Account
    {
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            Console.WriteLine("Running 'Account' example...");

            var latest = client.Account.Usage();
            Console.WriteLine("\nGot latest usage info: " + JsonConvert.SerializeObject(latest.Data));


            DateTimeOffset lastMonth = DateTimeOffset.Now.AddMonths(-1);
            var firstDayOfLastMonth = new DateTimeOffset(lastMonth.Year, lastMonth.Month, 1, 0, 0, 0, TimeSpan.Zero);

            var lastMonthUsage = client.Account.Usage(period: AccountUsagePeriod.Monthly, start: firstDayOfLastMonth);
            Console.WriteLine("\nGot last month's usage info: " + JsonConvert.SerializeObject(lastMonthUsage.Data));

        }
    }
}
