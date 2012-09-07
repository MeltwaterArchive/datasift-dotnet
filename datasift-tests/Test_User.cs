using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using datasift;

namespace datasift_tests
{
    [TestClass]
    public class Test_User
    {
        private User m_user = null;

        [TestInitialize()]
        public void InitTest()
        {
            m_user = new User(TestData.username, TestData.api_key);
            m_user.setApiClient(new MockApiClient(TestData.username, TestData.api_key));
        }

        [TestCleanup()]
        public void CleanupTest()
        {
            m_user = null;
        }

        [TestMethod]
        public void Test_Construction()
        {
            Assert.AreEqual(TestData.username, m_user.getUsername());
            Assert.AreEqual(TestData.api_key, m_user.getApiKey());
        }

        [TestMethod]
        public void Test_CreateDefinitionEmpty()
        {
            Definition def = m_user.createDefinition();
            Assert.AreEqual(String.Empty, def.get(), "Failed to create an empty definition");
        }

        [TestMethod]
        public void Test_CreateDefinition()
        {
            Definition def = m_user.createDefinition(TestData.definition);
            Assert.AreEqual(TestData.definition, def.get(), "Definition CSDL is not as expected");
        }

        [TestMethod]
        public void Test_RateLimits()
        {
            MockApiClient.setAPIResponse(
                new ApiResponse(
                    200, 
                    "{\"hash\":\"" + TestData.definition_hash + "\",\"created_at\":\"2011-12-13 14:15:16\",\"dpu\":10}",
                    200, 150));

            Definition def = m_user.createDefinition(TestData.definition);

            def.compile();

            Assert.AreEqual(200, m_user.getRateLimit(), "Rate limit is incorrect");
            Assert.AreEqual(150, m_user.getRateLimitRemaining(), "Rate limit remaining is incorrect");
        }

        [TestMethod]
        public void Test_GetUsage()
        {
            MockApiClient.setAPIResponse(
                new ApiResponse(
                    200,
                    "{\"start\":\"Mon, 07 Nov 2011 10:25:00 +0000\",\"end\":\"Mon, 07 Nov 2011 11:25:00 +0000\",\"streams\":{\"6fd9d61afba0149e0f1d42080ccd9075\":{\"licenses\":{\"twitter\":3},\"seconds\":300}}}",
                    200, 150));

            Usage u = m_user.getUsage("day");

            Assert.AreEqual(DateTime.ParseExact("2011-11-07 10:25:00", "yyyy-MM-dd HH:mm:ss", null), u.getStartDate(), "Usage start date is incorrect");
            Assert.AreEqual(DateTime.ParseExact("2011-11-07 11:25:00", "yyyy-MM-dd HH:mm:ss", null), u.getEndDate(), "Usage end date is incorrect");
            Assert.AreEqual(3, u.getLicenseUsage("6fd9d61afba0149e0f1d42080ccd9075", "twitter"), "Twitter license usage is incorrect");
            Assert.AreEqual(300, u.getSeconds("6fd9d61afba0149e0f1d42080ccd9075"), "Usage seconds is incorrect");
        }

        [TestMethod]
        public void Test_UsageWithApiErrors()
        {
            try
            {
                MockApiClient.setAPIResponse(
                    new ApiResponse(
                        400,
                        "{\"error\":\"Bad request from user supplied data\"}",
                        200, 150));
                Usage u = m_user.getUsage();
                Assert.Fail("Expected ApiException not thrown");
            }
            catch (ApiException e)
            {
                Assert.AreEqual("Bad request from user supplied data", e.Message, "400 exception messages is not as expected");
            }

            try
            {
                MockApiClient.setAPIResponse(
                    new ApiResponse(
                        401,
                        "{\"error\":\"User banned because they are a very bad person\"}",
                        200, 150));
                Usage u = m_user.getUsage();
                Assert.Fail("Expected ApiException not thrown");
            }
            catch (AccessDeniedException e)
            {
                Assert.AreEqual("User banned because they are a very bad person", e.Message, "401 exception messages is not as expected");
            }

            try
            {
                MockApiClient.setAPIResponse(
                    new ApiResponse(
                        404,
                        "{\"error\":\"Endpoint or data not found\"}",
                        200, 150));
                Usage u = m_user.getUsage();
                Assert.Fail("Expected ApiException not thrown");
            }
            catch (ApiException e)
            {
                Assert.AreEqual("Endpoint or data not found", e.Message, "404 exception messages is not as expected");
            }

            try
            {
                MockApiClient.setAPIResponse(
                    new ApiResponse(
                        500,
                        "{\"error\":\"Problem with an internal service\"}",
                        200, 150));
                Usage u = m_user.getUsage();
                Assert.Fail("Expected ApiException not thrown");
            }
            catch (ApiException e)
            {
                Assert.AreEqual("Problem with an internal service", e.Message, "500 exception messages is not as expected");
            }
        }
    }
}
