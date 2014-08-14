using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Collections.Generic;

namespace DataSiftTests
{
    [TestClass]
    public class Source : TestBase
    {
        public dynamic DummyParameters
        {
            get
            {
                return new
                {
                    likes = true,
                    posts_by_others = true,
                    comments = true,
                    page_likes = false
                };
            }
        }

        public dynamic DummyResources
        {
            get {

                var r = new[] {
                    new { 
                        parameters = new {
                            url = "http://www.facebook.com/theguardian",
                            title = "The Guardian",
                            id = 10513336322
                        }
                    }
                };

                return r;
            }
        }

        public dynamic DummyAuth
        {
            get
            {

                var r = new[] {
                    new { 
                        parameters = new {
                            value = "EZBXlFZBUgBYmjHkxc2pPmzLeJJYmAvQkwZCRdm0A1NAjidHy1h"
                        }
                    }
                };

                return r;
            }
        }

        private const string VALID_SOURCE_ID = "fa2e72e3a7ae40c2a6e86e96381d8165";
        private const string VALID_NAME = "Test source";
        private const string VALID_TYPE = "facebook_page";

        #region Get

        [TestMethod]
        public void Get_No_Arguments_Succeeds()
        {
            var response = Client.Source.Get();
            Assert.AreEqual(2, response.Data.sources.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Empty_Source_Type_Fails()
        {
            Client.Source.Get(sourceType: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Page_Is_Less_Than_One_Fails()
        {
            Client.Source.Get(page: 0);
        }

        [TestMethod]
        public void Get_Page_Succeeds()
        {
            var response = Client.Source.Get(page: 1);
            Assert.AreEqual(2, response.Data.count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Per_Page_Is_Less_Than_One_Fails()
        {
            Client.Source.Get(perPage: 0);
        }

        [TestMethod]
        public void Get_PerPage_Succeeds()
        {
            var response = Client.Source.Get(page: 1, perPage: 1);
            Assert.AreEqual(2, response.Data.count);
            Assert.AreEqual(1, response.Data.sources.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_By_Id_Empty_Id_Fails()
        {
            Client.Source.Get(id: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_By_Id_Bad_Format_Id_Fails()
        {
            Client.Source.Get(id: "get");
        }

        [TestMethod]
        public void Get_By_Id_Complete_Succeeds()
        {
            var response = Client.Source.Get(id: VALID_SOURCE_ID);
            Assert.AreEqual(VALID_SOURCE_ID, response.Data.id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Create

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Source_Type_Fails()
        {
            Client.Source.Create(null, VALID_NAME, DummyParameters, DummyResources, DummyAuth);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Source_Type_Fails()
        {
            Client.Source.Create("", VALID_NAME, DummyParameters, DummyResources, DummyAuth);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Name_Fails()
        {
            Client.Source.Create(VALID_TYPE, null, DummyParameters, DummyResources, DummyAuth);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Name_Fails()
        {
            Client.Source.Create(VALID_TYPE, "", DummyParameters, DummyResources, DummyAuth);
        }

        [TestMethod]
        public void Create_Correct_Args_Succeeds()
        {
            var response = Client.Source.Create(VALID_TYPE, VALID_NAME, DummyParameters, DummyResources, DummyAuth);
            Assert.AreEqual("da4f8df71a0f43698acf9240b5ad668f", response.Data.id);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        #endregion

        #region Update

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_Source_Type_Fails()
        {
            Client.Source.Update("da4f8df71a0f43698acf9240b5ad668f", sourceType: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_Name_Fails()
        {
            Client.Source.Update("da4f8df71a0f43698acf9240b5ad668f", name: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_Null_Id_Fails()
        {
            Client.Source.Update(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_Id_Fails()
        {
            Client.Source.Update("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Bad_Format_Id_Fails()
        {
            Client.Source.Update("update");
        }

        [TestMethod]
        public void Update_Correct_Args_Succeeds()
        {
            var response = Client.Source.Update("da4f8df71a0f43698acf9240b5ad668f", "facebook_page", "news_source", DummyParameters, DummyResources, DummyAuth);
            Assert.AreEqual("da4f8df71a0f43698acf9240b5ad668f", response.Data.id);
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
        }

        #endregion

        #region Delete

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_Null_Id_Fails()
        {
            Client.Source.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Empty_Id_Fails()
        {
            Client.Source.Delete("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Bad_Format_Id_Fails()
        {
            Client.Source.Delete("delete");
        }

        [TestMethod]
        public void Delete_Correct_Args_Succeeds()
        {
            var response = Client.Source.Delete("e25d533cf287ec44fe66e8362f61961f");
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region Start

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Start_Null_Id_Fails()
        {
            Client.Source.Start(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Start_Empty_Id_Fails()
        {
            Client.Source.Start("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Start_Bad_Format_Id_Fails()
        {
            Client.Source.Start("delete");
        }

        [TestMethod]
        public void Start_Correct_Args_Succeeds()
        {
            var response = Client.Source.Start("e25d533cf287ec44fe66e8362f61961f");
            Assert.AreEqual("active", response.Data.status);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Stop

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Stop_Null_Id_Fails()
        {
            Client.Source.Stop(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Stop_Empty_Id_Fails()
        {
            Client.Source.Stop("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Stop_Bad_Format_Id_Fails()
        {
            Client.Source.Stop("delete");
        }

        [TestMethod]
        public void Stop_Correct_Args_Succeeds()
        {
            var response = Client.Source.Stop("e25d533cf287ec44fe66e8362f61961f");
            Assert.AreEqual("paused", response.Data.status);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Log

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Log_Null_Id_Fails()
        {
            Client.Source.Log(id:null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Log_Empty_Id_Fails()
        {
            Client.Source.Log(id: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Log_Bad_Format_Id_Fails()
        {
            Client.Source.Log(id: "log");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Log_Page_Is_Less_Than_One_Fails()
        {
            Client.Source.Log(id: VALID_SOURCE_ID, page: 0);
        }

        [TestMethod]
        public void Log_Page_Succeeds()
        {
            var response = Client.Source.Log(id: VALID_SOURCE_ID, page: 1);
            Assert.AreEqual(20, response.Data.count);
            Assert.AreEqual(1, response.Data.log_entries.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Log_Per_Page_Is_Less_Than_One_Fails()
        {
            Client.Source.Log(id: VALID_SOURCE_ID, perPage: 0);
        }

        [TestMethod]
        public void Log_PerPage_Succeeds()
        {
            var response = Client.Source.Log(id: VALID_SOURCE_ID, page: 1, perPage: 1);
            Assert.AreEqual(20, response.Data.count);
            Assert.AreEqual(1, response.Data.log_entries.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Log_Correct_Args_Succeeds()
        {
            var response = Client.Source.Log(id: VALID_SOURCE_ID);
            Assert.AreEqual(20, response.Data.count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Resource Add

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResourceAdd_Null_Id_Fails()
        {
            Client.Source.ResourceAdd(null, DummyResources);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ResourceAdd_Empty_Id_Fails()
        {
            Client.Source.ResourceAdd("", DummyResources);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ResourceAdd_Bad_Format_Id_Fails()
        {
            Client.Source.ResourceAdd("source", DummyResources);
        }

        [TestMethod]
        public void ResourceAdd_Succeeds()
        {
            var resource = new[] {
                    new { 
                        parameters = new {
                            url = "http://www.facebook.com/theguardian",
                            title = "The Guardian",
                            id = 10513336322
                        }
                    }
                };


            var response = Client.Source.ResourceAdd(VALID_SOURCE_ID, resource);
            Assert.AreEqual("b801b1f3a6934cf29e02d092bff9b7f1", response.Data.resources[1].resource_id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        #endregion

        #region Resource Remove

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResourceRemove_Null_Id_Fails()
        {
            Client.Source.ResourceRemove(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ResourceRemove_Empty_Id_Fails()
        {
            Client.Source.ResourceRemove("", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ResourceRemove_Bad_Format_Id_Fails()
        {
            Client.Source.ResourceRemove("source", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResourceRemove_Null_ResourceIds_Fails()
        {
            Client.Source.ResourceRemove(VALID_SOURCE_ID, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ResourceRemove_Empty_ResourceIds_Fails()
        {
            Client.Source.ResourceRemove(VALID_SOURCE_ID, new string[]{});
        }

        [TestMethod]
        public void ResourceRemove_Succeeds()
        {
            var response = Client.Source.ResourceRemove(VALID_SOURCE_ID, new string[] { "b801b1f3a6934cf29e02d092bff9b7f1" });
            Assert.AreEqual(1, response.Data.resources.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Auth Add

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AuthAdd_Null_Id_Fails()
        {
            Client.Source.AuthAdd(null, DummyAuth);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthAdd_Empty_Id_Fails()
        {
            Client.Source.AuthAdd("", DummyAuth);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthAdd_Bad_Format_Id_Fails()
        {
            Client.Source.AuthAdd("source", DummyAuth);
        }

        [TestMethod]
        public void AuthAdd_Succeeds()
        {
            var auth = new[] {
                    new { 
                        parameters = new {
                            value = "AZBXlFZBUgBYmjHkxc2pPmzLeJJYmAvQkwZCRdm0A1NAjidHy1h"
                        }
                    }
                };

            var response = Client.Source.AuthAdd(VALID_SOURCE_ID, auth);
            Assert.AreEqual("c801b1f3a6934cf29e02d092bff9b7f1", response.Data.auth[1].identity_id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
         
        #endregion

        #region Auth Remove

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AuthRemove_Null_Id_Fails()
        {
            Client.Source.AuthRemove(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthRemove_Empty_Id_Fails()
        {
            Client.Source.AuthRemove("", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthRemove_Bad_Format_Id_Fails()
        {
            Client.Source.AuthRemove("source", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AuthRemove_Null_AuthIds_Fails()
        {
            Client.Source.AuthRemove(VALID_SOURCE_ID, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthRemove_Empty_AuthIds_Fails()
        {
            Client.Source.AuthRemove(VALID_SOURCE_ID, new string[] {});
        }

        [TestMethod]
        public void AuthRemove_Succeeds()
        {
            var response = Client.Source.AuthRemove(VALID_SOURCE_ID, new string[] { "c801b1f3a6934cf29e02d092bff9b7f1" });
            Assert.AreEqual(1, response.Data.auth.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion


    }
}
