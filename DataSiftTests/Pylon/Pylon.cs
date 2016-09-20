using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace DataSiftTests.Pylon
{
    [TestClass]
    public class Pylon : TestBase
    {
        public const string VALID_SERVICE = "facebook";
        private const string VALID_CSDL = "fb.content contains_any \"BMW, Mercedes, Cadillac\"";
        private const string VALID_ID = "12231f2f3825fe4c79e4f5def24c41fa8914f198";
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
            var response = Client.Pylon.Get(VALID_SERVICE);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(3, response.Data.subscriptions.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_By_ID_Empty_Fails()
        {
            Client.Pylon.Get(VALID_SERVICE, id: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_By_ID_Bad_Format_Fails()
        {
            Client.Pylon.Get(VALID_SERVICE, id: "invalid");
        }

        [TestMethod]
        public void Get_By_ID_Succeeds()
        {
            var response = Client.Pylon.Get(VALID_SERVICE, id: VALID_ID);
            Assert.AreEqual(VALID_ID, response.Data.id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Page_Is_Less_Than_One_Fails()
        {
            Client.Pylon.Get(VALID_SERVICE, page: 0);
        }

        [TestMethod]
        public void Get_Page_Succeeds()
        {
            var response = Client.Pylon.Get(VALID_SERVICE, page: 1);
            Assert.AreEqual(3, response.Data.subscriptions.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Per_Page_Is_Less_Than_One_Fails()
        {
            Client.Pylon.Get(VALID_SERVICE, perPage: 0);
        }

        [TestMethod]
        public void Get_PerPage_Succeeds()
        {
            var response = Client.Pylon.Get(VALID_SERVICE, page: 1, perPage: 3);
            Assert.AreEqual(3, response.Data.subscriptions.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Validate

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Validate_Null_CSDL_Fails()
        {
            Client.Pylon.Validate(VALID_SERVICE, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Validate_Empty_CSDL_Fails()
        {
            Client.Pylon.Validate(VALID_SERVICE, "");
        }

        [TestMethod]
        public void Validate_Complete_CSDL_Succeeds()
        {
            var response = Client.Pylon.Validate(VALID_SERVICE, VALID_CSDL);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Compile

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Compile_Null_CSDL_Fails()
        {
            Client.Pylon.Compile(VALID_SERVICE, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Compile_Empty_CSDL_Fails()
        {
            Client.Pylon.Compile(VALID_SERVICE, "");
        }

        [TestMethod]
        public void Compile_Complete_CSDL_Succeeds()
        {
            var response = Client.Pylon.Compile(VALID_SERVICE, VALID_CSDL);
            Assert.AreEqual("58eb8c4b74257406547ab1ed3be346a8", response.Data.hash);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Start

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Start_Empty_Hash_Fails()
        {
            Client.Pylon.Start(VALID_SERVICE, hash: "", name: VALID_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Start_Invalid_Hash_Fails()
        {
            Client.Pylon.Start(VALID_SERVICE, hash: "invalid", name: VALID_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Start_Empty_Name_Fails()
        {
            var response = Client.Pylon.Start(VALID_SERVICE, hash: VALID_HASH, name: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Start_Empty_ID_Fails()
        {
            Client.Pylon.Start(VALID_SERVICE, id: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Start_Invalid_ID_Fails()
        {
            Client.Pylon.Start(VALID_SERVICE, id: "invalid");
        }

        [TestMethod]
        public void Start_Succeeds()
        {
            var response = Client.Pylon.Start(VALID_SERVICE, hash: VALID_HASH, name: VALID_NAME);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("f78491987d92bef86e8b0cee5a64b3ab360f059d", response.Data.id);
        }

        #endregion

        #region Stop

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Stop_Null_ID_Fails()
        {
            Client.Pylon.Stop(VALID_SERVICE, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Stop_Empty_ID_Fails()
        {
            Client.Pylon.Stop(VALID_SERVICE, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Stop_Invalid_ID_Fails()
        {
            Client.Pylon.Stop(VALID_SERVICE, "invalid");
        }

        [TestMethod]
        public void Stop_Succeeds()
        {
            var response = Client.Pylon.Stop(VALID_SERVICE, VALID_ID);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region Update

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_Null_ID_Fails()
        {
            Client.Pylon.Update(VALID_SERVICE, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_ID_Fails()
        {
            Client.Pylon.Update(VALID_SERVICE, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Invalid_ID_Fails()
        {
            Client.Pylon.Update(VALID_SERVICE, "invalid");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_Hash_Fails()
        {
            Client.Pylon.Update(VALID_SERVICE, VALID_ID, hash: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Invalid_Hash_Fails()
        {
            Client.Pylon.Update(VALID_SERVICE, VALID_ID, hash: "invalid");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_Name_Fails()
        {
            var response = Client.Pylon.Update(VALID_SERVICE, VALID_ID,  name: "");
        }

        [TestMethod]
        public void Update_Succeeds()
        {
            var response = Client.Pylon.Update(VALID_SERVICE, id: VALID_ID, hash: VALID_HASH, name: VALID_NAME);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region Analyze

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Analyze_Null_ID_Fails()
        {
            Client.Pylon.Analyze(VALID_SERVICE, null, DummyParameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Analyze_Empty_ID_Fails()
        {
            Client.Pylon.Analyze(VALID_SERVICE, "", DummyParameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Analyze_Invalid_ID_Fails()
        {
            Client.Pylon.Analyze(VALID_SERVICE, "invalid", DummyParameters);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Analyze_Empty_Filter_Fails()
        {
            Client.Pylon.Analyze(VALID_SERVICE, VALID_ID, DummyParameters, filter: "");
        }

        [TestMethod]
        public void Analyze_With_Null_Filter_Succeeds()
        {
            var response = Client.Pylon.Analyze(VALID_SERVICE, VALID_ID, DummyParameters, filter: null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Analyze_With_Filter_Succeeds()
        {
            var response = Client.Pylon.Analyze(VALID_SERVICE, VALID_ID, DummyParameters, filter: "interaction.content contains 'apple'");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Analyze_Too_Late_Start_Fails()
        {
            Client.Pylon.Analyze(VALID_SERVICE, VALID_ID, DummyParameters, start: DateTimeOffset.Now.AddDays(1), end: DateTimeOffset.Now.AddDays(3));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Analyze_Too_Late_End_Fails()
        {
            Client.Pylon.Analyze(VALID_SERVICE, VALID_ID, DummyParameters, start: VALID_START, end: DateTimeOffset.Now.AddDays(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Analyze_End_Before_Start_Fails()
        {
            Client.Pylon.Analyze(VALID_SERVICE, VALID_ID, DummyParameters, start: VALID_START, end: DateTimeOffset.Now.AddDays(-31));
        }

        [TestMethod]
        public void Analyze_With_Null_Start_Succeeds()
        {
            var response = Client.Pylon.Analyze(VALID_SERVICE, VALID_ID, DummyParameters, start: null, end: DateTimeOffset.Now);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Analyze_With_Null_End_Succeeds()
        {
            var response = Client.Pylon.Analyze(VALID_SERVICE, VALID_ID, DummyParameters, start: DateTimeOffset.Now.AddDays(-1), end: null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Analyze_With_Start_And_End_Succeeds()
        {
            var response = Client.Pylon.Analyze(VALID_SERVICE, VALID_ID, DummyParameters, start: DateTimeOffset.Now.AddDays(-1), end: DateTimeOffset.Now);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Analyze_With_Null_Parameters_Fails()
        {
            Client.Pylon.Analyze(VALID_SERVICE, VALID_ID, parameters: null, start: DateTimeOffset.Now.AddDays(-1), end: DateTimeOffset.Now);
        }


        [TestMethod]
        public void Analyze_Succeeds()
        {
            var response = Client.Pylon.Analyze(VALID_SERVICE, VALID_ID, DummyParameters);
            Assert.AreEqual(false, response.Data.analysis.redacted);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Analyze_Nested()
        {
            dynamic nested = new {
                    analysis_type = "freqDist",
                    parameters = new
                    {
                        threshold = 3,
                        target = "fb.author.gender"
                    },
                    child = new {
                        parameters = new
                        {
                            threshold = 3,
                            target = "fb.author.age"
                        }
                    }
                };

            var response = Client.Pylon.Analyze(VALID_SERVICE, "12231f2f3825fe4c79e4f5def24c41fa89nested", nested);
            Assert.AreEqual(false, response.Data.analysis.redacted);
            Assert.AreEqual("freqDist", response.Data.analysis.results[0].child.analysis_type);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Tags

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Tags_Null_ID_Fails()
        {
            Client.Pylon.Tags(VALID_SERVICE, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Tags_Empty_ID_Fails()
        {
            Client.Pylon.Tags(VALID_SERVICE, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Tags_Invalid_ID_Fails()
        {
            Client.Pylon.Tags(VALID_SERVICE, "invalid");
        }

        [TestMethod]
        public void Tags_Succeeds()
        {
            var response = Client.Pylon.Tags(VALID_SERVICE, VALID_ID);
            Assert.AreEqual("tag.one", response.Data[0]);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Sample

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Sample_Null_ID_Fails()
        {
            Client.Pylon.Sample(VALID_SERVICE, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Sample_Empty_ID_Fails()
        {
            Client.Pylon.Sample(VALID_SERVICE, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Sample_ID_Bad_Format_Fails()
        {
            Client.Pylon.Sample(VALID_SERVICE, "invalid");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Sample_Too_Low_Count_Fails()
        {
            Client.Pylon.Sample(VALID_SERVICE, VALID_ID, 9);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Sample_Too_High_Count_Fails()
        {
            Client.Pylon.Sample(VALID_SERVICE, VALID_ID, 101);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Sample_Too_Late_Start_Fails()
        {
            Client.Pylon.Sample(VALID_SERVICE, VALID_ID, start: DateTimeOffset.Now.AddDays(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Sample_Too_Late_End_Fails()
        {
            Client.Pylon.Sample(VALID_SERVICE, VALID_ID, end: DateTimeOffset.Now.AddDays(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Sample_End_Before_Start_Fails()
        {
            Client.Pylon.Sample(VALID_SERVICE, VALID_ID, start: VALID_START, end: DateTimeOffset.Now.AddDays(-31));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Sample_Empty_Filter_Fails()
        {
            Client.Pylon.Sample(VALID_SERVICE, VALID_ID, filter: "");
        }

        [TestMethod]
        public void Sample_Succeeds()
        {
            var response = Client.Pylon.Sample(VALID_SERVICE, VALID_ID);
            Assert.AreEqual(80, response.Data.remaining);
            Assert.AreEqual("en", response.Data.interactions[0].fb.language);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

    }
}
