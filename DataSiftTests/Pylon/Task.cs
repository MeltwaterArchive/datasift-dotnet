using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace DataSiftTests.Pylon
{
    [TestClass]
    public class Task : TestBase
    {
        public const string VALID_SERVICE = "facebook";
        public const string VALID_TASK_ID = "6763dd472cf7e5af9cf1752627f11710a5335953";
        public const string VALID_RECORDING_ID = "e9dde04774540ac119c2317a4d15a8b3a1350937";
        public const string VALID_NAME = "New task";
        public const string VALID_TYPE = "analysis";

        public dynamic VALID_PARAMETERS
        {
            get
            {
                return new
                {
                    parameters = new
                    {
                        analysis_type = "timeSeries",
                        parameters = new
                        {
                            interval = "hour",
                            span = 1
                        }
                    }
                };
            }
        }

        #region Get (GET /v1.4/pylon/<service>/task[?per_page=<per_page>&page=<page>&status=<status>])

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Get_Null_Service_Fails()
        {
            Client.Pylon.Task.Get(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Empty_Service_Fails()
        {
            Client.Pylon.Task.Get("");
        }

        [TestMethod]
        public void Get_Valid_Service_Succeeds()
        {
            var response = Client.Pylon.Task.Get(VALID_SERVICE);
            Assert.AreEqual("analysis", response.Data.tasks[0].type);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Page_Is_Less_Than_One_Fails()
        {
            Client.Pylon.Task.Get(VALID_SERVICE, page: 0);
        }

        [TestMethod]
        public void Get_Page_Succeeds()
        {
            var response = Client.Pylon.Task.Get(VALID_SERVICE, page: 1);
            Assert.AreEqual(3, response.Data.tasks.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Per_Page_Is_Less_Than_One_Fails()
        {
            Client.Pylon.Task.Get(VALID_SERVICE, perPage: 0);
        }

        [TestMethod]
        public void Get_PerPage_Succeeds()
        {
            var response = Client.Pylon.Task.Get(VALID_SERVICE, page: 1, perPage: 3);
            Assert.AreEqual(3, response.Data.tasks.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region GET /v1.4/pylon/<service>/task/<id>

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetOne_Null_Service_Fails()
        {
            Client.Pylon.Task.Get(null, taskId: VALID_TASK_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetOne_Empty_Service_Fails()
        {
            Client.Pylon.Task.Get("", taskId: VALID_TASK_ID);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetOne_Empty_Id_Fails()
        {
            Client.Pylon.Task.Get(VALID_SERVICE, taskId: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetOne_Invalid_Id_Fails()
        {
            Client.Pylon.Task.Get(VALID_SERVICE, taskId: "taskId");
        }

        [TestMethod]
        public void GetOne_Succeeds()
        {
            var response = Client.Pylon.Task.Get(VALID_SERVICE, taskId: VALID_TASK_ID);
            Assert.AreEqual(VALID_TASK_ID, response.Data.id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region POST /v1.4/pylon/<service>/task

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Service_Fails()
        {
            Client.Pylon.Task.Create(null, VALID_RECORDING_ID, VALID_NAME, VALID_TYPE, VALID_PARAMETERS);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Service_Fails()
        {
            Client.Pylon.Task.Create("", VALID_RECORDING_ID, VALID_NAME, VALID_TYPE, VALID_PARAMETERS);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_RecordingId_Fails()
        {
            Client.Pylon.Task.Create(VALID_SERVICE, null, VALID_NAME, VALID_TYPE, VALID_PARAMETERS);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_RecordingId_Fails()
        {
            Client.Pylon.Task.Create(VALID_SERVICE, "", VALID_NAME, VALID_TYPE, VALID_PARAMETERS);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Invalid_RecordingId_Fails()
        {
            Client.Pylon.Task.Create(VALID_SERVICE, "recordingId", VALID_NAME, VALID_TYPE, VALID_PARAMETERS);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Name_Fails()
        {
            Client.Pylon.Task.Create(VALID_SERVICE, VALID_RECORDING_ID, null, VALID_TYPE, VALID_PARAMETERS);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Name_Fails()
        {
            Client.Pylon.Task.Create(VALID_SERVICE, VALID_RECORDING_ID, "", VALID_TYPE, VALID_PARAMETERS);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Type_Fails()
        {
            Client.Pylon.Task.Create(VALID_SERVICE, VALID_RECORDING_ID, VALID_NAME, null, VALID_PARAMETERS);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Type_Fails()
        {
            Client.Pylon.Task.Create(VALID_SERVICE, VALID_RECORDING_ID, VALID_NAME, "", VALID_PARAMETERS);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Parameters_Fails()
        {
            Client.Pylon.Task.Create(VALID_SERVICE, VALID_RECORDING_ID, VALID_NAME, VALID_TYPE, null);
        }

        [TestMethod]
        public void Create_Succeeds()
        {
            var response = Client.Pylon.Task.Create(VALID_SERVICE, VALID_RECORDING_ID, VALID_NAME, VALID_TYPE, VALID_PARAMETERS);
            Assert.AreEqual(VALID_TASK_ID, response.Data.id);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        #endregion


    }
}
