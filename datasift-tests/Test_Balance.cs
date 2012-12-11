using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace datasift_tests
{
    public class Balance_Tests
    {
        public const string ABalance_json = "{\"balance\":{\"credit\":9.98,\"plan\":\"free\",\"threshold\":2}}";
    }


    [TestClass]
    public class BalanceConstruct : Balance_Tests
    {
        [TestMethod]
        public void Test_BalanceConstruct()
        {
            new datasift.Balance(new datasift.JSONdn(ABalance_json));
        }
    }

    [TestClass]
    public class Balance : Balance_Tests
    {

        [TestMethod]
        public void Test_BalanceCredit()
        {

        }
    }
}
