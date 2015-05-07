using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace DataSiftTests
{
    [TestClass]
    public class Pylon : TestBase
    {

        private const string VALID_CSDL = "fb.content contains_any \"BMW, Mercedes, Cadillac\"";
        private const string VALID_HASH = "58eb8c4b74257406547ab1ed3be346a8";
        private const string VALID_NAME = "Example recording";
        private DateTimeOffset VALID_START = DateTimeOffset.Now.AddDays(-30);
        private DateTimeOffset VALID_END = DateTimeOffset.Now;

        public dynamic DummyParameters
        {
            get
            {
                return new
                {
                    analysis_type = "freqDist",
                    parameters = new
                    {
                        threshold = 5,
                        target = "fb.author.age"
                    }
                };
            }
        }

        #region Get

        [TestMethod]
        public void Get_Succeeds()
        {
            var response = Client.Pylon.Get();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_By_Hash_Empty_Fails()
        {
            Client.Pylon.Get(hash: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_By_Hash_Bad_Format_Fails()
        {
            Client.Pylon.Get(hash: "invalid");
        }

        [TestMethod]
        public void Get_By_Hash_Complete_Succeeds()
        {
            var response = Client.Pylon.Get(hash: VALID_HASH);
            Assert.AreEqual(VALID_HASH, response.Data.hash);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        #endregion

        #region Validate

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Validate_Null_CSDL_Fails()
        {
            Client.Pylon.Validate(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Validate_Empty_CSDL_Fails()
        {
            Client.Pylon.Validate("");
        }

        [TestMethod]
        public void Validate_Complete_CSDL_Succeeds()
        {
            var response = Client.Pylon.Validate(VALID_CSDL);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Compile

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Compile_Null_CSDL_Fails()
        {
            Client.Pylon.Compile(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Compile_Empty_CSDL_Fails()
        {
            Client.Pylon.Compile("");
        }

        [TestMethod]
        public void Compile_Complete_CSDL_Succeeds()
        {
            var response = Client.Pylon.Compile(VALID_CSDL);
            Assert.AreEqual("58eb8c4b74257406547ab1ed3be346a8", response.Data.hash);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Start

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Start_Null_Hash_Fails()
        {
            Client.Pylon.Start(null, VALID_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Start_Empty_Hash_Fails()
        {
            Client.Pylon.Start("", VALID_NAME);
        }

        [TestMethod]
        public void Start_Null_Name_Succeeds()
        {
            var response = Client.Pylon.Start(VALID_HASH, null);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Start_Empty_Name_Fails()
        {
            var response = Client.Pylon.Start(VALID_HASH, "");
        }

        [TestMethod]
        public void Start_Succeeds()
        {
            var response = Client.Pylon.Start(VALID_HASH, VALID_NAME);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region Stop

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Stop_Null_Hash_Fails()
        {
            Client.Pylon.Stop(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Stop_Empty_Hash_Fails()
        {
            Client.Pylon.Stop("");
        }

        [TestMethod]
        public void Stop_Succeeds()
        {
            var response = Client.Pylon.Stop(VALID_HASH);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region Analyze

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Analyze_Null_Hash_Fails()
        {
            Client.Pylon.Analyze(null, DummyParameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Analyze_Empty_Hash_Fails()
        {
            Client.Pylon.Analyze("", DummyParameters);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Analyze_Empty_Filter_Fails()
        {
            Client.Pylon.Analyze(VALID_HASH, DummyParameters, filter: "");
        }

        [TestMethod]
        public void Analyze_With_Null_Filter_Succeeds()
        {
            var response = Client.Pylon.Analyze(VALID_HASH, DummyParameters, filter: null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Analyze_With_Filter_Succeeds()
        {
            var response = Client.Pylon.Analyze(VALID_HASH, DummyParameters, filter: "interaction.content contains 'apple'");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Analyze_Too_Late_Start_Fails()
        {
            Client.Pylon.Analyze(VALID_HASH, DummyParameters, start: DateTimeOffset.Now.AddDays(1), end: DateTimeOffset.Now.AddDays(3));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Analyze_Too_Late_End_Fails()
        {
            Client.Pylon.Analyze(VALID_HASH, DummyParameters, start: VALID_START, end: DateTimeOffset.Now.AddDays(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Analyze_End_Before_Start_Fails()
        {
            Client.Pylon.Analyze(VALID_HASH, DummyParameters, start: VALID_START, end: DateTimeOffset.Now.AddDays(-31));
        }

        [TestMethod]
        public void Analyze_With_Null_Start_Succeeds()
        {
            var response = Client.Pylon.Analyze(VALID_HASH, DummyParameters, start: null, end: DateTimeOffset.Now);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Analyze_With_Null_End_Succeeds()
        {
            var response = Client.Pylon.Analyze(VALID_HASH, DummyParameters, start: DateTimeOffset.Now.AddDays(-1), end: null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Analyze_With_Start_And_End_Succeeds()
        {
            var response = Client.Pylon.Analyze(VALID_HASH, DummyParameters, start: DateTimeOffset.Now.AddDays(-1), end: DateTimeOffset.Now);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Analyze_With_Null_Parameters_Fails()
        {
            Client.Pylon.Analyze(VALID_HASH, parameters: null, start: DateTimeOffset.Now.AddDays(-1), end: DateTimeOffset.Now);
        }


        [TestMethod]
        public void Analyze_Succeeds()
        {
            var response = Client.Pylon.Analyze(VALID_HASH, DummyParameters);
            Assert.AreEqual(false, response.Data.analysis.redacted);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Tags

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Tags_Null_Hash_Fails()
        {
            Client.Pylon.Tags(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Tags_Empty_Hash_Fails()
        {
            Client.Pylon.Tags("");
        }

        [TestMethod]
        public void Tags_Succeeds()
        {
            var response = Client.Pylon.Tags(VALID_HASH);
            Assert.AreEqual("tag.one", response.Data[0]);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion
    }
}
