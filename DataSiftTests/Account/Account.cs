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
        public void Last_Months_Usage_Succeeds()
        {
            DateTimeOffset lastMonth = DateTimeOffset.Now.AddMonths(-1);
            var firstDayOfLastMonth = new DateTimeOffset(lastMonth.Year, lastMonth.Month, 1, 0, 0, 0, TimeSpan.Zero);
            var firstDayOfThisMonth = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, 1, 0, 0, 0, TimeSpan.Zero);
            
            var response = Client.Account.Usage(firstDayOfLastMonth, firstDayOfThisMonth, period: AccountUsagePeriod.Monthly);
            Assert.AreEqual(0.03332, response.Data.usage[1].quantity);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

    }
}
