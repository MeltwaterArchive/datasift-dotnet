using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using datasift;

namespace datasift_tests
{
    [TestClass]
    public class Test_Definition
    {
        private User m_user = null;

        [TestInitialize()]
        public void InitTest()
        {
            m_user = new User(TestData.username, TestData.api_key);
            m_user.setApiClient(new User.CallAPIDelegate(MockApiClient.callAPI));
        }

        [TestCleanup()]
        public void CleanupTest()
        {
            m_user = null;
        }

        [TestMethod]
        public void Test_Construction()
        {
            Definition def = new Definition(m_user);
            Assert.AreEqual("", def.get(), "Default CSDL is not empty");
        }

        [TestMethod]
        public void Test_ConstructionWithCSDL()
        {
            Definition def = new Definition(m_user, TestData.definition);
            Assert.AreEqual(TestData.definition, def.get(), "Definition CSDL is not as expected");
        }

        [TestMethod]
        public void Test_CreateDefinition()
        {
            Definition def = new Definition(m_user);
            Assert.AreEqual("", def.get(), "Default definition CSDL is not empty");
            def.set(TestData.definition);
            Assert.AreEqual(TestData.definition, def.get(), "Definition CSDL not set correctly");
        }

        [TestMethod]
        public void Test_CompileSuccess()
        {
            MockApiClient.setAPIResponse(
                new ApiResponse(
                    200,
                    "{\"hash\":\"" + TestData.definition_hash + "\",\"created_at\":\"2011-12-13 14:15:16\",\"dpu\":10}",
                    200, 150));

            Definition def = new Definition(m_user, TestData.definition);
            Assert.AreEqual(TestData.definition, def.get(), "Definition CSDL not set correctly");

            try
            {
                def.compile();
            }
            catch (InvalidDataException e)
            {
                Assert.Fail("InvalidDataException: " + e.Message);
            }
            catch (CompileFailedException e)
            {
                Assert.Fail("CompileFailedException: " + e.Message);
            }
            catch (ApiException e)
            {
                Assert.Fail("ApiException: " + e.Message);
            }

            Assert.AreEqual(200, m_user.getRateLimit(), "Incorrect rate limit");
            Assert.AreEqual(150, m_user.getRateLimitRemaining(), "Incorrect rate limit remaining");
            Assert.AreEqual(TestData.definition_hash, def.getHash(), "Incorrect hash");
            Assert.AreEqual(DateTime.ParseExact("2011-12-13 14:15:16", "yyyy-MM-dd HH:mm:ss", null), def.getCreatedAt(), "Incorrect created at date");
            Assert.AreEqual(10, def.getTotalDpu(), "Incorrect total DPU");
        }

        [TestMethod]
        public void Test_CompileFailure()
        {
            MockApiClient.setAPIResponse(
                new ApiResponse(
                    400,
                    "{\"error\":\"The target interactin.content does not exist\"}",
                    200, 150));

            Definition def = new Definition(m_user, TestData.invalid_definition);
            Assert.AreEqual(TestData.invalid_definition, def.get(), "Definition CSDL not set correctly");

            try
            {
                def.compile();
                Assert.Fail("Expected CompileFailedException was not thrown");
            }
            catch (InvalidDataException e)
            {
                Assert.Fail("InvalidDataException: " + e.Message);
            }
            catch (CompileFailedException e)
            {
                Assert.AreEqual("The target interactin.content does not exist", e.Message, "Incorrect compile error message in the CompileFailedException");
            }
            catch (ApiException e)
            {
                Assert.Fail("ApiException: " + e.Message);
            }
        }

        [TestMethod]
        public void Test_CompileSuccessThenFailure()
        {
            MockApiClient.setAPIResponse(
                new ApiResponse(
                    200,
                    "{\"hash\":\"" + TestData.definition_hash + "\",\"created_at\":\"2011-12-13 14:15:16\",\"dpu\":10}",
                    200, 150));

            Definition def = new Definition(m_user, TestData.definition);
            Assert.AreEqual(TestData.definition, def.get(), "Definition CSDL not set correctly");

            try
            {
                def.compile();
            }
            catch (InvalidDataException e)
            {
                Assert.Fail("InvalidDataException: " + e.Message);
            }
            catch (CompileFailedException e)
            {
                Assert.Fail("CompileFailedException: " + e.Message);
            }
            catch (ApiException e)
            {
                Assert.Fail("ApiException: " + e.Message);
            }

            Assert.AreEqual(200, m_user.getRateLimit(), "Incorrect rate limit");
            Assert.AreEqual(150, m_user.getRateLimitRemaining(), "Incorrect rate limit remaining");
            Assert.AreEqual(TestData.definition_hash, def.getHash(), "Incorrect hash");
            Assert.AreEqual(DateTime.ParseExact("2011-12-13 14:15:16", "yyyy-MM-dd HH:mm:ss", null), def.getCreatedAt(), "Incorrect created at date");
            Assert.AreEqual(10, def.getTotalDpu(), "Incorrect total DPU");

            def.set(TestData.invalid_definition);
            Assert.AreEqual(TestData.invalid_definition, def.get(), "Definition CSDL not set correctly");

            MockApiClient.setAPIResponse(
                new ApiResponse(
                    400,
                    "{\"error\":\"The target interactin.content does not exist\"}",
                    200, 150));

            try
            {
                def.compile();
                Assert.Fail("Expected CompileFailedException was not thrown");
            }
            catch (InvalidDataException e)
            {
                Assert.Fail("InvalidDataException: " + e.Message);
            }
            catch (CompileFailedException e)
            {
                Assert.AreEqual("The target interactin.content does not exist", e.Message, "Incorrect compile error message in the CompileFailedException");
            }
            catch (ApiException e)
            {
                Assert.Fail("ApiException: " + e.Message);
            }
        }

        [TestMethod]
        public void Test_GetDpuBreakdown()
        {
            MockApiClient.setAPIResponse(
                new ApiResponse(
                    200,
                    "{\"hash\":\"" + TestData.definition_hash + "\",\"created_at\":\"2011-12-13 14:15:16\",\"dpu\":10}",
                    200, 150));

            Definition def = new Definition(m_user, TestData.definition);
            Assert.AreEqual(TestData.definition, def.get(), "Definition CSDL not set correctly");
            Assert.AreEqual(TestData.definition_hash, def.getHash(), "Incorrect hash");

            MockApiClient.setAPIResponse(
                new ApiResponse(
                    200,
                    "{\"detail\":{\"contains\":{\"count\":1,\"dpu\":4,\"targets\":{\"interaction.content\":{\"count\":1,\"dpu\":4}}}},\"dpu\":4}",
                    200, 150));

            Dpu dpu = def.getDpuBreakdown();

            Assert.AreEqual(4, dpu.getTotal(), "The total DPU is incorrect");
            Assert.AreEqual(1, dpu.getDpu().Count, "Incorrect number of detail items");
            Assert.AreEqual(1, dpu.getDpu()["contains"].getCount(), "Incorrect count for contains");
            Assert.AreEqual(4, dpu.getDpu()["contains"].getDpu(), "Incorrect DPU for contains");
            Assert.AreEqual(1, dpu.getDpu()["contains"].getTargets()["interaction.content"].getCount(), "Incorrect count for interaction.content");
            Assert.AreEqual(4, dpu.getDpu()["contains"].getTargets()["interaction.content"].getDpu(), "Incorrect DPU for interaction.content");
        }

        [TestMethod]
        public void Test_GetBuffered()
        {
            MockApiClient.setAPIResponse(
                new ApiResponse(
                    200,
                    "{\"hash\":\"" + TestData.definition_hash + "\",\"created_at\":\"2011-12-13 14:15:16\",\"dpu\":10}",
                    200, 150));

            Definition def = new Definition(m_user, TestData.definition);
            Assert.AreEqual(TestData.definition, def.get(), "Definition CSDL not set correctly");
            Assert.AreEqual(TestData.definition_hash, def.getHash(), "Incorrect hash");

            MockApiClient.setAPIResponse(
                new ApiResponse(
                    200,
                    "{\"stream\":[{\"interaction\":{\"source\":\"Snaptu\",\"author\":{\"username\":\"nittolexia\",\"name\":\"nittosoetreznoe\",\"id\":172192091,\"avatar\":\"http://a0.twimg.com/profile_images/1429378181/gendowor_normal.jpg\",\"link\":\"http://twitter.com/nittolexia\"},\"type\":\"twitter\",\"link\":\"http://twitter.com/nittolexia/statuses/89571192838684672\",\"created_at\":\"Sat, 09 Jul 2011 05:46:51 +0000\",\"content\":\"RT @ayyuchadel: Haha RT @nittolexia: Mending gak ush maen twitter dehh..RT @sansan_arie:\",\"id\":\"1e0a9eedc207acc0e074ea8aecb2c5ea\"},\"twitter\":{\"user\":{\"name\":\"nittosoetreznoe\",\"description\":\"fuck all\",\"location\":\"denpasar, bali\",\"statuses_count\":6830,\"followers_count\":88,\"friends_count\":111,\"screen_name\":\"nittolexia\",\"lang\":\"en\",\"time_zone\":\"Alaska\",\"id\":172192091,\"geo_enabled\":true},\"mentions\":[\"ayyuchadel\",\"nittolexia\",\"sansan_arie\"],\"id\":\"89571192838684672\",\"text\":\"RT @ayyuchadel: Haha RT @nittolexia: Mending gak ush maen twitter dehh..RT @sansan_arie:\",\"source\":\"<a href=\\\"http://www.snaptu.com\\\" rel=\\\"nofollow\\\">Snaptu</a>\",\"created_at\":\"Sat, 09 Jul 2011 05:46:51 +0000\"},\"klout\":{\"score\":45,\"network\":55,\"amplification\":17,\"true_reach\":31,\"slope\":0,\"class\":\"Networker\"},\"peerindex\":{\"score\":30},\"language\":{\"tag\":\"da\"}}]}",
                    200, 150));

            Interaction[] interactions = def.getBuffered();
            Assert.AreEqual("nittosoetreznoe", interactions[0].getStringVal("twitter.user.name"), "The Twitter username is incorrect");
            Assert.AreEqual(89571192838684672, interactions[0].getLongVal("twitter.id"), "The Twitter ID is incorrect");
            Assert.AreEqual("http://a0.twimg.com/profile_images/1429378181/gendowor_normal.jpg", interactions[0].getStringVal("interaction.author.avatar"), "The author avatar is incorrect");
        }
    }
}
