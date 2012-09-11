using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using datasift;

namespace datasift_tests
{
    [TestClass]
    public class Test_PushOutputParams
    {
        [TestInitialize()]
        public void InitTest()
        {
            TestData.init();
        }

        [TestMethod]
        public void Test_SetAndGet()
        {
            PushOutputParams pop = new PushOutputParams();
            // Set data
            foreach (string key in TestData.push_output_params.Keys)
            {
                pop.set(key, TestData.push_output_params[key]);
            }
            // Get data
            foreach (string key in TestData.push_output_params.Keys)
            {
                Assert.AreEqual(TestData.push_output_params[key], pop[key], "Stored value for '" + key + "' is incorrect");
            }
        }

        [TestMethod]
        public void Test_Parse()
        {
            JSONdn json = new JSONdn("{\"delivery_frequency\":10,\"url\":\"http://www.example.com/push_endpoint\",\"auth\":{\"type\":\"basic\",\"username\":\"wooop\",\"password\":\"dsadsa\"}}");

            PushOutputParams pop = new PushOutputParams(json);

            Assert.AreEqual("10", pop["delivery_frequency"], "Parsed value for 'delivery_frequency' is incorrect");
            Assert.AreEqual("http://www.example.com/push_endpoint", pop["url"], "Parsed value for 'url' is incorrect");
            Assert.AreEqual("basic", pop["auth.type"], "Parsed value for 'auth.type' is incorrect");
            Assert.AreEqual("wooop", pop["auth.username"], "Parsed value for 'auth.username' is incorrect");
            Assert.AreEqual("dsadsa", pop["auth.password"], "Parsed value for 'auth.password' is incorrect");
        }
    }
}
