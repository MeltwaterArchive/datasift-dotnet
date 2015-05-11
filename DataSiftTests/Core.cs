using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace DataSiftTests
{
    [TestClass]
    public class Core : TestBase
    {
        private const string VALID_APIKEY = "b09z345fe2f1fed748c12268fd473662";
        private const string VALID_HISTORICS_ID = "9e97d9ac115e58fcb7ab";
        private const string VALID_USERNAME = "username";
        private const string VALID_CSDL = "interaction.content contains \"music\"";
        private const string VALID_STREAM_HASH = "08b923395b6ce8bfa4d96f57f863a1c3";

        #region Instatiate Client

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void New_With_Null_Username_Fails()
        {
            new DataSift.DataSiftClient(null, VALID_APIKEY);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void New_With_Empty_Username_Fails()
        {
            new DataSift.DataSiftClient("", VALID_APIKEY);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void New_With_Null_Apikey_Fails()
        {
            new DataSift.DataSiftClient(VALID_USERNAME, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void New_With_Empty_Apikey_Fails()
        {
            new DataSift.DataSiftClient(VALID_USERNAME, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void New_With_Bad_Apikey_Fails()
        {
            new DataSift.DataSiftClient(VALID_USERNAME, "key");
        }

        [TestMethod]
        public void New_With_Valid_Args_Succeeds()
        {
            new DataSift.DataSiftClient(VALID_USERNAME, VALID_APIKEY);
        }

        #endregion

        #region Validate

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Validate_Null_CSDL_Fails()
        {
            Client.Validate(null);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Validate_Empty_CSDL_Fails()
        {
            Client.Validate("");
        }

        [TestMethod]
        public void Validate_Complete_CSDL_Succeeds()
        {
            var response = Client.Validate(VALID_CSDL);
            Assert.AreEqual("0.1", response.Data.dpu);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Compile

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Compile_Null_CSDL_Fails()
        {
            Client.Compile(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Compile_Empty_CSDL_Fails()
        {
            Client.Compile("");
        }

        [TestMethod]
        public void Compile_Complete_CSDL_Succeeds()
        {
            var response = Client.Compile(VALID_CSDL);
            Assert.AreEqual("0.1", response.Data.dpu);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Usage

        [TestMethod]
        public void Usage()
        {
            var response = Client.Usage();

            var streams = (IDictionary<string, object>)response.Data.streams;
            var stream = (dynamic)streams["693f5134c73a62ed85ef271040bf266b"];

            Assert.AreEqual(3600, stream.seconds);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region DPU

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DPU_Null_Hash_And_HistoricsId_Fails()
        {
            Client.DPU(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DPU_Both_Hash_And_HistoricsId_Fails()
        {
            Client.DPU(VALID_STREAM_HASH, VALID_HISTORICS_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DPU_Empty_Hash_Fails()
        {
            Client.DPU(hash: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DPU_Bad_Format_Hash_Fails()
        {
            Client.DPU(hash: "hash");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DPU_Empty_HistoricsId_Fails()
        {
            Client.DPU(historicsId: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DPU_Bad_Format_HistoricsId_Fails()
        {
            Client.DPU("historicsId");
        }

        [TestMethod]
        public void DPU_Complete_Hash_Succeeds()
        {
            var response = Client.DPU(hash: VALID_STREAM_HASH);
            Assert.AreEqual(2, response.Data.detail.contains.count);
            Assert.AreEqual(0.2, response.Data.detail.contains.dpu);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void DPU_Complete_HistoricsId_Succeeds()
        {
            var response = Client.DPU(historicsId: VALID_HISTORICS_ID);
            Assert.AreEqual(2, response.Data.detail.contains.count);
            Assert.AreEqual(0.2, response.Data.detail.contains.dpu);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Balance

        [TestMethod]
        public void Balance()
        {
            var response = Client.Balance();
            Assert.AreEqual("Platinum", response.Data.balance.plan);
            Assert.AreEqual(249993.7, response.Data.balance.remaining_dpus);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Pull

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Pull_Null_Id_Fails()
        {
            Client.Pull(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Pull_Empty_Id_Fails()
        {
            Client.Pull("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Pull_Bad_Format_Id_Fails()
        {
            Client.Pull("pull");
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Pull_Size_Less_Than_One_Fails()
        {
            Client.Pull(VALID_STREAM_HASH, size: 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Pull_Empty_Cursor_Fails()
        {
            Client.Pull(VALID_STREAM_HASH, cursor: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Pull_Bad_Format_Cursor_Fails()
        {
            Client.Pull(VALID_STREAM_HASH, cursor: "cursor");
        }

        [TestMethod]
        public void Pull_Correct_Args__JsonMetaFormat_Succeeds()
        {
            var response = Client.Pull("08b923395b6ce8bfa4d96f57jsonmeta", size: 100000, cursor: "3b29a57fa62474d2c3cd4ca55510c4fe");
            Assert.AreEqual("johndoe", response.Data.interactions[0].interaction.author.username);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Pull_Correct_Args__JsonArrayFormat_Succeeds()
        {
            var response = Client.Pull("08b923395b6ce8bfa4d96f5jsonarray", size: 100000, cursor: "3b29a57fa62474d2c3cd4ca55510c4fe");
            Assert.AreEqual("johndoe", response.Data[0].interaction.author.username);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Pull_Correct_Args__JsonNewlineFormat_Succeeds()
        {
            var response = Client.Pull("08b923395b6ce8bfa4d96jsonnewline", size: 100000, cursor: "3b29a57fa62474d2c3cd4ca55510c4fe");
            Assert.AreEqual("johndoe", response.Data[0].interaction.author.username);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        #endregion
    }
}
