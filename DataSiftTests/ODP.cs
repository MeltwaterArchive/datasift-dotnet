using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace DataSiftTests
{
    [TestClass]
    public class ODP : TestBase
    {
        private const string VALID_SOURCE_ID = "fa2e72e3a7ae40c2a6e86e96381d8165";
        
        public dynamic DummyData
        {
            get
            {
                return new[] {
                    new { 
                        title = "Dummy content"
                    }
                };
            }
        }

        #region Ingestion

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ingest_Null_SourceId_Fails()
        { 
            Client.ODP.Ingest(null, DummyData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ingest_Empty_SourceId_Fails()
        {
            Client.ODP.Ingest("", DummyData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ingest_Bad_SourceId_Fails()
        {
            Client.ODP.Ingest("ingest", DummyData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ingest_Null_Data_Fails()
        {
            Client.ODP.Ingest(VALID_SOURCE_ID, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ingest_NonArray_Data_Fails()
        {
            Client.ODP.Ingest(VALID_SOURCE_ID, new { value = 1 });
        }

        [TestMethod]
        public void Ingest_Correct_Args_Succeeds()
        {
            var response = Client.ODP.Ingest(VALID_SOURCE_ID, DummyData);
            Assert.AreEqual(1, response.Data.accepted);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

    }
}
