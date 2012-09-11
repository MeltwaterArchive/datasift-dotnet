using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using datasift;

namespace datasift_tests
{
    [TestClass]
    public class Test_Historic
    {
        private User m_user = null;
        private Historic m_historic = null;

        [TestInitialize()]
        public void InitTest()
        {
            TestData.init();
            m_user = new User(TestData.username, TestData.api_key);
            m_user.setApiClient(new MockApiClient(TestData.username, TestData.api_key));
            m_historic = m_user.createHistoric(
                TestData.definition_hash,
                TestData.historic_start,
                TestData.historic_end,
                TestData.historic_sources,
                TestData.historic_sample,
                TestData.historic_name);
        }

        [TestCleanup()]
        public void CleanupTest()
        {
            m_historic = null;
            m_user = null;
        }

        [TestMethod]
        public void Test_Construction()
        {
            Assert.AreEqual(TestData.definition_hash, m_historic.getStreamHash(), "Stream hash is incorrect");
            Assert.AreEqual(TestData.historic_name, m_historic.getName(), "Name is incorrect");
            Assert.AreEqual(TestData.historic_start, m_historic.getStartDate(), "Start date is incorrect");
            Assert.AreEqual(TestData.historic_end, m_historic.getEndDate(), "End date is incorrect");
            Assert.AreEqual("created", m_historic.getStatus(), "Status is incorrect");
            Assert.AreEqual(0, m_historic.getProgress(), "Progress is incorrect");
            Assert.AreEqual(TestData.historic_sample, m_historic.getSample(), "Sample is incorrect");
        }
    }
}
