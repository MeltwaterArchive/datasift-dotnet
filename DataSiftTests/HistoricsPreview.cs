using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Collections.Generic;
using DataSift.Rest;

namespace DataSiftTests
{
    [TestClass]
    public class HistoricsPreview : TestBase
    {

        private const string VALID_STREAM_HASH = "2459b03a13577579bca76471778a5c3d";
        private string[] VALID_SOURCES = new string[] { "twitter" };
        private DateTimeOffset VALID_START = DateTimeOffset.Now.AddDays(-2);

        public List<HistoricsPreviewParameter> DummyCreateParams
        {
            get
            {
                var prms = new List<HistoricsPreviewParameter>();
                prms.Add(new HistoricsPreviewParameter() { Target = "interaction.author.link", Analysis = "targetVol", Argument = "hour" });
                return prms;
            }
        }

        #region Create

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Sources_Fails()
        {
            Client.HistoricsPreview.Create(VALID_STREAM_HASH, null, DummyCreateParams, VALID_START);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Sources_Fails()
        {
            Client.HistoricsPreview.Create(VALID_STREAM_HASH, new string[] { }, DummyCreateParams, VALID_START);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Hash_Fails()
        {
            Client.HistoricsPreview.Create(null, VALID_SOURCES, DummyCreateParams, VALID_START);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Hash_Fails()
        {
            Client.HistoricsPreview.Create("", VALID_SOURCES, DummyCreateParams, VALID_START);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Bad_Format_Hash_Fails()
        {
            Client.HistoricsPreview.Create("hash", VALID_SOURCES, DummyCreateParams, VALID_START);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Start_Too_Early_Fails()
        {
            Client.HistoricsPreview.Create(VALID_STREAM_HASH, VALID_SOURCES, DummyCreateParams, new DateTimeOffset(2009, 12, 31, 23, 59, 59, TimeSpan.Zero));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Start_Too_Late_Fails()
        {
            Client.HistoricsPreview.Create(VALID_STREAM_HASH, VALID_SOURCES, DummyCreateParams, DateTimeOffset.Now.AddHours(-1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Start_After_End_Fails()
        {
            Client.HistoricsPreview.Create(VALID_STREAM_HASH, VALID_SOURCES, DummyCreateParams, VALID_START, DateTimeOffset.Now.AddDays(-3));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_End_Too_Late_Fails()
        {
            Client.HistoricsPreview.Create(VALID_STREAM_HASH, VALID_SOURCES, DummyCreateParams, VALID_START, DateTimeOffset.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Params_Fails()
        {
            Client.HistoricsPreview.Create(VALID_STREAM_HASH, VALID_SOURCES, null, VALID_START);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Params_Fails()
        {
            Client.HistoricsPreview.Create(VALID_STREAM_HASH, VALID_SOURCES, new List<HistoricsPreviewParameter>(), VALID_START);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_More_Than_Twenty_Params_Fails()
        {
            var prms = new List<HistoricsPreviewParameter>();

            for (int i = 0; i <= 20; i++)
            {
                prms.Add(new HistoricsPreviewParameter() { Target = "interaction.author.link", Analysis = "targetVol", Argument = "hour" });
            }

            Client.HistoricsPreview.Create(VALID_STREAM_HASH, VALID_SOURCES, prms, VALID_START);
        }

        [TestMethod]
        public void Create_Correct_Args_Succeeds()
        {
            var response = Client.HistoricsPreview.Create(VALID_STREAM_HASH, VALID_SOURCES, DummyCreateParams, VALID_START);
            Assert.AreEqual("3ddb72ca02389dbf3b46", response.Data.id);
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
        }

        #endregion

        #region Get

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Get_Null_Id_Fails()
        {
            Client.HistoricsPreview.Get(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Empty_Id_Fails()
        {
            Client.HistoricsPreview.Get("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Bad_Format_Id_Fails()
        {
            Client.HistoricsPreview.Get("get");
        }

        [TestMethod]
        public void Get_Running_Succeeds()
        {
            var response = Client.HistoricsPreview.Get("e25d533cf287ec44fe66e8362running");
            Assert.AreEqual("running", response.Data.status);
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
        }

        [TestMethod]
        public void Get_Finished_Succeeds()
        {
            var response = Client.HistoricsPreview.Get("e25d533cf287ec44fe66e8362finished");
            Assert.AreEqual("succeeded", response.Data.status);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

    }
}
