using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace DataSiftTests.Account
{
    [TestClass]
    public class Token : TestBase
    {
        public const string VALID_IDENTITY = "f3865ceea4bac3f7eec0ea12d7e83508";
        public const string VALID_SERVICE = "facebook";
        public const string VALID_TOKEN = "fed85f6b316fb18930ee28e8754f4963";

        #region Get

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Get_Null_IdentityId_Fails()
        {
            Client.Account.Identity.Token.Get(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Empty_IdentityId_Fails()
        {
            Client.Account.Identity.Token.Get("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Invalid_IdentityId_Fails()
        {
            Client.Account.Identity.Token.Get("invalid ID");
        }

        [TestMethod]
        public void Get_Valid_IdentityId_Succeeds()
        {
            var response = Client.Account.Identity.Token.Get(VALID_IDENTITY);
            Assert.AreEqual(VALID_SERVICE, response.Data.tokens[0].service);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Empty_Service_Fails()
        {
            Client.Account.Identity.Token.Get(VALID_IDENTITY, service: "");
        }

        [TestMethod]
        public void Get_Valid_Service_Succeeds()
        {
            var response = Client.Account.Identity.Token.Get(VALID_IDENTITY, service: VALID_SERVICE);
            Assert.AreEqual(VALID_SERVICE, response.Data.tokens[0].service);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Page_Is_Less_Than_One_Fails()
        {
            Client.Account.Identity.Token.Get(VALID_IDENTITY, page: 0);
        }

        [TestMethod]
        public void Get_Page_Succeeds()
        {
            var response = Client.Account.Identity.Token.Get(VALID_IDENTITY, page: 1);
            Assert.AreEqual(1, response.Data.tokens.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Per_Page_Is_Less_Than_One_Fails()
        {
            Client.Account.Identity.Token.Get(VALID_IDENTITY, perPage: 0);
        }

        [TestMethod]
        public void Get_PerPage_Succeeds()
        {
            var response = Client.Account.Identity.Token.Get(VALID_IDENTITY, page: 1, perPage: 1);
            Assert.AreEqual(1, response.Data.tokens.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Create

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_IdentityId_Fails()
        {
            Client.Account.Identity.Token.Create(null, VALID_SERVICE, VALID_TOKEN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_IdentityId_Fails()
        {
            Client.Account.Identity.Token.Create("", VALID_SERVICE, VALID_TOKEN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Invalid_IdentityId_Fails()
        {
            Client.Account.Identity.Token.Create("invalid ID", VALID_SERVICE, VALID_TOKEN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Service_Fails()
        {
            Client.Account.Identity.Token.Create(VALID_IDENTITY, null, VALID_TOKEN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Service_Fails()
        {
            Client.Account.Identity.Token.Create(VALID_IDENTITY, "", VALID_TOKEN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Token_Fails()
        {
            Client.Account.Identity.Token.Create(VALID_IDENTITY, VALID_SERVICE, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Token_Fails()
        {
            Client.Account.Identity.Token.Create(VALID_IDENTITY, VALID_SERVICE, "");
        }

        [TestMethod]
        public void Create_Succeeds()
        {
            var response = Client.Account.Identity.Token.Create(VALID_IDENTITY, VALID_SERVICE, VALID_TOKEN);
            Assert.AreEqual(VALID_TOKEN, response.Data.token);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        #endregion

        #region Update

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_Null_IdentityId_Fails()
        {
            Client.Account.Identity.Token.Update(null, VALID_SERVICE, VALID_TOKEN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_IdentityId_Fails()
        {
            Client.Account.Identity.Token.Update("", VALID_SERVICE, VALID_TOKEN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Invalid_IdentityId_Fails()
        {
            Client.Account.Identity.Token.Update("invalid ID", VALID_SERVICE, VALID_TOKEN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_Null_Service_Fails()
        {
            Client.Account.Identity.Token.Update(VALID_IDENTITY, null, VALID_TOKEN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_Service_Fails()
        {
            Client.Account.Identity.Token.Update(VALID_IDENTITY, "", VALID_TOKEN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_Null_Token_Fails()
        {
            Client.Account.Identity.Token.Update(VALID_IDENTITY, VALID_SERVICE, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_Token_Fails()
        {
            Client.Account.Identity.Token.Update(VALID_IDENTITY, VALID_SERVICE, "");
        }

        [TestMethod]
        public void Update_Succeeds()
        {
            var response = Client.Account.Identity.Token.Update(VALID_IDENTITY, VALID_SERVICE, "ddd85f6b316fb18930ee28e8754f4963");
            Assert.AreEqual("ddd85f6b316fb18930ee28e8754f4963", response.Data.token);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Delete

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_Null_IdentityId_Fails()
        {
            Client.Account.Identity.Token.Delete(null, VALID_SERVICE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Empty_IdentityId_Fails()
        {
            Client.Account.Identity.Token.Delete("", VALID_SERVICE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Invalid_IdentityId_Fails()
        {
            Client.Account.Identity.Token.Delete("invalid ID", VALID_SERVICE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_Null_Service_Fails()
        {
            Client.Account.Identity.Token.Delete(VALID_IDENTITY, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Empty_Service_Fails()
        {
            Client.Account.Identity.Token.Delete(VALID_IDENTITY, "");
        }

        [TestMethod]
        public void Delete_Succeeds()
        {
            var response = Client.Account.Identity.Token.Delete(VALID_IDENTITY, VALID_SERVICE);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

    }
}
