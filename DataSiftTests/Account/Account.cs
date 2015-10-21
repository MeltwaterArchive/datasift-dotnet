using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using DataSift.Enum;

namespace DataSiftTests.Account
{
    [TestClass]
    public class Account : TestBase
    {
        #region Usage

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_End_Before_Start_Fails()
        {
            Client.Account.Usage(start: DateTimeOffset.Now, end: DateTimeOffset.Now.AddHours(-1));
        }

        [TestMethod]
        public void Usage_Succeeds()
        {
            var response = Client.Account.Usage();
            Assert.AreEqual(0.02312, response.Data.usage[0].quantity);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Last_Months_Usage_Succeeds()
        {
            DateTimeOffset date = DateTimeOffset.Now.AddMonths(-1);
            var firstDayOfLastMonth = new DateTimeOffset(date.Year, date.Month, 1, 0, 0, 0, TimeSpan.Zero);
           
            var response = Client.Account.Usage(period: AccountUsagePeriod.Monthly, start: firstDayOfLastMonth);
            Assert.AreEqual(0.03332, response.Data.usage[1].quantity);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

    }
}
