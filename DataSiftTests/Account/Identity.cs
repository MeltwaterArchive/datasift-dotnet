using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace DataSiftTests.Account
{
    [TestClass]
    public class Identity : TestBase
    {

        private const string VALID_LABEL = "Customer 1";
        public const string VALID_ID = "f3865ceea4bac3f7eec0ea12d7e83508";
        private const DataSift.Enum.IdentityStatus VALID_STATUS = DataSift.Enum.IdentityStatus.Active;
        private const bool VALID_MASTER = false;

        #region Create

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Label_Fails()
        {
            Client.Account.Identity.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Label_Fails()
        {
            Client.Account.Identity.Create("");
        }

        [TestMethod]
        public void Create_Valid_Status_Succeeds()
        {
            var response = Client.Account.Identity.Create(VALID_LABEL, VALID_STATUS, VALID_MASTER);
            Assert.AreEqual(VALID_LABEL, response.Data.label);
            Assert.AreEqual(VALID_MASTER, response.Data.master);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void Create_Succeeds()
        {
            var response = Client.Account.Identity.Create(VALID_LABEL);
            Assert.AreEqual(VALID_LABEL, response.Data.label);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        #endregion

        #region Get

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Empty_ID_Fails()
        {
            Client.Account.Identity.Get(id: "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Invalid_ID_Fails()
        {
            Client.Account.Identity.Get(id: "invalid ID");
        }

        [TestMethod]
        public void Get_Valid_ID_Succeeds()
        {
            var response = Client.Account.Identity.Get(id: VALID_ID);
            Assert.AreEqual(VALID_ID, response.Data.id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Empty_Label_Fails()
        {
            Client.Account.Identity.Get(label: "");
        }

        [TestMethod]
        public void Get_Null_Label_Succeeds()
        {
            var response = Client.Account.Identity.Get(label: null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Get_Valid_Label_Succeeds()
        {
            var response = Client.Account.Identity.Get(label: VALID_LABEL);
            Assert.AreEqual(VALID_LABEL, response.Data.identities[0].label);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Page_Is_Less_Than_One_Fails()
        {
            Client.Account.Identity.Get(page: 0);
        }

        [TestMethod]
        public void Get_Page_Succeeds()
        {
            var response = Client.Account.Identity.Get(page: 1);
            Assert.AreEqual(2, response.Data.identities.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Per_Page_Is_Less_Than_One_Fails()
        {
            Client.Account.Identity.Get(perPage: 0);
        }

        [TestMethod]
        public void Get_PerPage_Succeeds()
        {
            var response = Client.Account.Identity.Get(page: 1, perPage: 1);
            Assert.AreEqual(2, response.Data.identities.Count);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Get_Succeeds()
        {
            var response = Client.Account.Identity.Get();
            Assert.AreEqual("a7e06ebce923e84c7817e91e07082113", response.Data.identities[0].id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Update

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_Null_ID_Fails()
        {
            Client.Account.Identity.Update(null, VALID_LABEL);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_ID_Fails()
        {
            Client.Account.Identity.Update("", VALID_LABEL);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Invalid_ID_Fails()
        {
            Client.Account.Identity.Update("invalid", VALID_LABEL);
        }

        [TestMethod]
        public void Update_Null_Label_Succeeds()
        {
            var response = Client.Account.Identity.Update(VALID_ID, null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_Label_Fails()
        {
            Client.Account.Identity.Update(VALID_ID, "");
        }

        [TestMethod]
        public void Update_Succeeds()
        {
            var response = Client.Account.Identity.Update(VALID_ID, VALID_LABEL, VALID_STATUS, VALID_MASTER);
            Assert.AreEqual(VALID_ID, response.Data.id);
            Assert.AreEqual(VALID_LABEL, response.Data.label);
            Assert.AreEqual("active", response.Data.status);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Delete

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_Null_ID_Fails()
        {
            Client.Account.Identity.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Empty_ID_Fails()
        {
            Client.Account.Identity.Delete("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Invalid_ID_Fails()
        {
            Client.Account.Identity.Delete("invalid");
        }

        [TestMethod]
        public void DeleteSucceeds()
        {
            var response = Client.Account.Identity.Delete(VALID_ID);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

    }
}
