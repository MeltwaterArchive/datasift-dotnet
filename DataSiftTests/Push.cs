using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using DataSift.Enum;

namespace DataSiftTests
{
    [TestClass]
    public class Push : TestBase
    {
        private const string VALID_PUSH_ID = "d668655cfe5f93741ddcd30bb309a8c7";
        private const string VALID_STREAM_HASH = "13e9347e7da32f19fcdb08e297019d2e";
        private const string VALID_HISTORICS_ID = "6cd38099f4c1e0f1ac31";
        private const string VALID_NAME = "New subscription";
        private const string VALID_TYPE = "pull";

        #region Get

        [TestMethod]
        public void Get_No_Arguments_Succeeds()
        {
            var response = Client.Push.Get();
            Assert.AreEqual(2, response.Data.count);
            Assert.AreEqual(VALID_PUSH_ID, response.Data.subscriptions[0].id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_By_Id_Empty_Id_Fails()
        {
            Client.Push.Get(id: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_By_Id_Bad_Format_Id_Fails()
        {
            Client.Push.Get(id: "push");
        }

        [TestMethod]
        public void Get_By_Id_Complete_Succeeds()
        {
            var response = Client.Push.Get(id: VALID_PUSH_ID);
            Assert.AreEqual("d468655cfe5f93741ddcd30bb309a8c7", response.Data.id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_By_Hash_Empty_Hash_Fails()
        {
            Client.Push.Get(hash: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_By_Hash_Bad_Format_Hash_Fails()
        {
            Client.Push.Get(hash: "push");
        }

        [TestMethod]
        public void Get_By_Hash_Complete_Succeeds()
        {
            var response = Client.Push.Get(hash: VALID_STREAM_HASH);
            Assert.AreEqual(VALID_STREAM_HASH, response.Data.subscriptions[0].hash);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_By_HistoricsId_Empty_HistoricsId_Fails()
        {
            Client.Push.Get(historicsId: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_By_HistoricsId_Bad_Format_HistoricsId_Fails()
        {
            Client.Push.Get(historicsId: "push");
        }

        [TestMethod]
        public void Get_By_HistoricsId_Complete_Succeeds()
        {
            var response = Client.Push.Get(historicsId: VALID_HISTORICS_ID);
            Assert.AreEqual("3a5c2546136a037d4b2df0b8b8836f3e", response.Data.subscriptions[0].id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Page_Is_Less_Than_One_Fails()
        {
            Client.Push.Get(page: 0);
        }

        [TestMethod]
        public void Get_Page_Succeeds()
        {
            var response = Client.Push.Get(page: 1);
            Assert.AreEqual(2, response.Data.count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Per_Page_Is_Less_Than_One_Fails()
        {
            Client.Push.Get(perPage: 0);
        }

        [TestMethod]
        public void Get_PerPage_Succeeds()
        {
            var response = Client.Push.Get(page: 1, perPage: 1);
            Assert.AreEqual(2, response.Data.count);
            Assert.AreEqual(1, response.Data.subscriptions.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Validate

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Validate_Null_Type_Fails()
        {
            Client.Push.Validate(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Validate_Empty_Type_Fails()
        {
            Client.Push.Validate("");
        }

        #endregion

        #region Create

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Name_Fails()
        {
            Client.Push.Create(null, VALID_TYPE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Name_Fails()
        {
            Client.Push.Create("", VALID_TYPE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Type_Fails()
        {
            Client.Push.Create(VALID_NAME, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Type_Fails()
        {
            Client.Push.Create(VALID_NAME, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_End_Before_Start_Fails()
        {
            Client.Push.Create(VALID_NAME, "", start: DateTimeOffset.Now, end: DateTimeOffset.Now.AddHours(-1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Neither_Hash_Nor_HistoricsId_Fails()
        {
            Client.Push.Create(VALID_NAME, VALID_TYPE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Both_Hash_And_HistoricsId_Fails()
        {
            Client.Push.Create(VALID_NAME, VALID_TYPE, hash: VALID_STREAM_HASH, historicsId: VALID_HISTORICS_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Hash_Fails()
        {
            Client.Push.Create(VALID_NAME, VALID_TYPE, hash: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Bad_Format_Hash_Fails()
        {
            Client.Push.Create(VALID_NAME, VALID_TYPE, hash: "hash");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_HistoricsId_Fails()
        {
            Client.Push.Create(VALID_NAME, VALID_TYPE, historicsId: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Bad_Format_HistoricsId_Fails()
        {
            Client.Push.Create(VALID_NAME, VALID_TYPE, historicsId: "historics");
        }

        [TestMethod]
        public void Create_Correct_Args_Succeeds()
        {
            var response = Client.Push.Create(VALID_NAME, VALID_TYPE, hash: VALID_STREAM_HASH, initialStatus: PushStatus.Active, start: DateTimeOffset.Now, end: DateTimeOffset.Now.AddHours(1));
            Assert.AreEqual("d468655cfe5f93741ddcd30bb309a8c7", response.Data.id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        #endregion

        #region Delete

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_Null_Id_Fails()
        {
            Client.Push.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Empty_Id_Fails()
        {
            Client.Push.Delete("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Bad_Format_Id_Fails()
        {
            Client.Push.Delete("delete");
        }

        [TestMethod]
        public void Delete_Correct_Args_Succeeds()
        {
            var response = Client.Push.Delete(VALID_PUSH_ID);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region Stop

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Stop_Null_Id_Fails()
        {
            Client.Push.Stop(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Stop_Empty_Id_Fails()
        {
            Client.Push.Stop("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Stop_Bad_Format_Id_Fails()
        {
            Client.Push.Stop("stop");
        }

        [TestMethod]
        public void Stop_Correct_Args_Succeeds()
        {
            var response = Client.Push.Stop(VALID_PUSH_ID);
            Assert.AreEqual("d468655cfe5f93741ddcd30bb309a8c7", response.Data.id);
            Assert.AreEqual("finishing", response.Data.status);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Pause

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Pause_Null_Id_Fails()
        {
            Client.Push.Pause(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Pause_Empty_Id_Fails()
        {
            Client.Push.Pause("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Pause_Bad_Format_Id_Fails()
        {
            Client.Push.Pause("delete");
        }

        [TestMethod]
        public void Pause_Correct_Args_Succeeds()
        {
            var response = Client.Push.Pause(VALID_PUSH_ID);
            Assert.AreEqual("d468655cfe5f93741ddcd30bb309a8c7", response.Data.id);
            Assert.AreEqual("paused", response.Data.status);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Resume

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Resume_Null_Id_Fails()
        {
            Client.Push.Resume(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Resume_Empty_Id_Fails()
        {
            Client.Push.Resume("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Resume_Bad_Format_Id_Fails()
        {
            Client.Push.Resume("delete");
        }

        [TestMethod]
        public void Resume_Correct_Args_Succeeds()
        {
            var response = Client.Push.Resume(VALID_PUSH_ID);
            Assert.AreEqual("d468655cfe5f93741ddcd30bb309a8c7", response.Data.id);
            Assert.AreEqual("active", response.Data.status);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Log

        [TestMethod]
        public void Log_No_Arguments_Succeeds()
        {
            var response = Client.Push.Log();
            Assert.AreEqual(8740, response.Data.count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Log_By_Id_Empty_Id_Fails()
        {
            Client.Push.Log(id: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Log_By_Id_Bad_Format_Id_Fails()
        {
            Client.Push.Log(id: "push");
        }

        [TestMethod]
        public void Log_By_Id_Complete_Succeeds()
        {
            var response = Client.Push.Log(id: VALID_PUSH_ID);
            Assert.AreEqual("d468655cfe5f93741ddcd30bb309a8c7", response.Data.log_entries[0].subscription_id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Log_Page_Is_Less_Than_One_Fails()
        {
            Client.Push.Log(page: 0);
        }

        [TestMethod]
        public void Log_Page_Succeeds()
        {
            var response = Client.Push.Log(page: 1);
            Assert.AreEqual(182, response.Data.count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Log_Per_Page_Is_Less_Than_One_Fails()
        {
            Client.Push.Log(perPage: 0);
        }

        [TestMethod]
        public void Log_PerPage_Succeeds()
        {
            var response = Client.Push.Log(page: 1, perPage: 5, orderDirection: OrderDirection.Ascending);
            Assert.AreEqual(5, response.Data.log_entries.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Update

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_Null_Id_Fails()
        {
            Client.Push.Update(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_Id_Fails()
        {
            Client.Push.Update("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Bad_Format_Id_Fails()
        {
            Client.Push.Update("update");
        }

        [TestMethod]
        public void Update_Correct_Args_Succeeds()
        {
            var response = Client.Push.Update(VALID_PUSH_ID, "new name");
            Assert.AreEqual("new name", response.Data.name);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

    }
}
