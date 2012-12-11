using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace datasift_tests
{
    [TestClass]
    public class Test_Balance
    {
        [TestMethod]
        public void Test_BalanceConstruct()
        {
            new datasift.Balance(new datasift.JSONdn("{\"balance\":{\"credit\":10,\"plan\":\"free\",\"threshold\":2}}"));
        }
    }
}
