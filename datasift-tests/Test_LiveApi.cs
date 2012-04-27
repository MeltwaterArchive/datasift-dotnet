using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using datasift;

namespace datasift_tests
{
    [TestClass]
    public class Test_LiveApi
    {
        private User m_user = null;

        [TestInitialize()]
        public void InitTest()
        {
            m_user = new User(TestData.username, TestData.api_key);
        }

        [TestCleanup()]
        public void CleanupTest()
        {
            m_user = null;
        }

        [TestMethod]
        public void Test_CompileSuccess()
        {
            Definition def = m_user.createDefinition(TestData.definition);
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

            Assert.AreEqual(TestData.definition_hash, def.getHash(), "Incorrect hash");
            Assert.IsTrue(def.getTotalDpu() > 0, "The total DPU is <= 0");
        }

        [TestMethod]
        public void Test_CompileFailure()
        {
            Definition def = m_user.createDefinition(TestData.invalid_definition);
            Assert.AreEqual(TestData.invalid_definition, def.get(), "Definition CSDL not set correctly");

            try
            {
                def.compile();
                Assert.Fail("Expected CompileFailedException not thrown");
            }
            catch (InvalidDataException e)
            {
                Assert.Fail("InvalidDataException: " + e.Message);
            }
            catch (CompileFailedException)
            {
                // Expected exception
            }
            catch (ApiException e)
            {
                Assert.Fail("ApiException: " + e.Message);
            }
        }

        [TestMethod]
        public void Test_CompileSuccessThenFailure()
        {
            Definition def = m_user.createDefinition(TestData.definition);
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

            Assert.AreEqual(TestData.definition_hash, def.getHash(), "Incorrect hash");

            def.set(TestData.invalid_definition);
            Assert.AreEqual(TestData.invalid_definition, def.get(), "Definition CSDL not set correctly");

            try
            {
                def.compile();
                Assert.Fail("Expected CompileFailedException not thrown");
            }
            catch (InvalidDataException e)
            {
                Assert.Fail("InvalidDataException: " + e.Message);
            }
            catch (CompileFailedException)
            {
                // Expected exception
            }
            catch (ApiException e)
            {
                Assert.Fail("ApiException: " + e.Message);
            }
        }
    }
}
