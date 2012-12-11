using System;
using NUnit.Framework;

namespace datasift_tests
{
    public class Balance_Tests
    {
        public const string ABalance_json = "{\"balance\":{\"credit\":9.98,\"plan\":\"free\",\"threshold\":2}}";
    }


    [TestFixture]
    public class BalanceConstruct : Balance_Tests
    {
        [Test]
        public void Test_BalanceConstruct()
        {
            new datasift.Balance(new datasift.JSONdn(ABalance_json));
        }
    }

    [TestFixture]
    public class Balance : Balance_Tests
    {
        datasift.Balance balance;

        [SetUp]
        public void setup()
        {
            balance = new datasift.Balance(new datasift.JSONdn(ABalance_json));
        }

        [Test]
        public void Test_BalanceCredit()
        {
            Assert.That(balance.getCredit(), Is.InstanceOf<double>());
            Assert.That(balance.getCredit(), Is.EqualTo(9.98));
        }

        [Test]
        public void Test_BalanceThreshold()
        {
            Assert.That(balance.getThreshold(), Is.InstanceOf<double>());
            Assert.That(balance.getThreshold(), Is.EqualTo(2));
        }
        [Test]
        public void Test_BalancePlan()
        {
            Assert.That(balance.getPlan(), Is.InstanceOf<string>());
            Assert.That(balance.getPlan(), Is.EqualTo("free"));
        }

    }
}
