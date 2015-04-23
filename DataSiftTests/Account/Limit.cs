using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace DataSiftTests.Account
{
    [TestClass]
    public class Limit : TestBase
    {
        public const string VALID_IDENTITY = "f3865ceea4bac3f7eec0ea12d7e83508";
        public const string VALID_SERVICE = "facebook";
        public const int VALID_TOTAL_ALLOWANCE = 100000;

        #region Get

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Empty_IdentityId_Fails()
        {
            Client.Account.Identity.Limit.Get(VALID_SERVICE, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Invalid_IdentityId_Fails()
        {
            Client.Account.Identity.Limit.Get(VALID_SERVICE, "invalid ID");
        }

        [TestMethod]
        public void Get_Valid_IdentityId_Succeeds()
        {
            var response = Client.Account.Identity.Limit.Get(VALID_SERVICE, VALID_IDENTITY);
            Assert.AreEqual(VALID_TOTAL_ALLOWANCE, response.Data.total_allowance);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Get_Null_Service_Fails()
        {
            Client.Account.Identity.Limit.Get(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Empty_Service_Fails()
        {
            Client.Account.Identity.Limit.Get("");
        }

        [TestMethod]
        public void Get_Valid_Service_Succeeds()
        {
            var response = Client.Account.Identity.Limit.Get(VALID_SERVICE);
            Assert.AreEqual(VALID_SERVICE, response.Data.limits[0].service);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Page_Is_Less_Than_One_Fails()
        {
            Client.Account.Identity.Limit.Get(VALID_SERVICE, page: 0);
        }

        [TestMethod]
        public void Get_Page_Succeeds()
        {
            var response = Client.Account.Identity.Limit.Get(VALID_SERVICE, page: 1);
            Assert.AreEqual(1, response.Data.page);
            Assert.AreEqual(VALID_TOTAL_ALLOWANCE, response.Data.limits[1].total_allowance);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Per_Page_Is_Less_Than_One_Fails()
        {
            Client.Account.Identity.Limit.Get(VALID_SERVICE, perPage: 0);
        }

        [TestMethod]
        public void Get_PerPage_Succeeds()
        {
            var response = Client.Account.Identity.Limit.Get(VALID_SERVICE, page: 1, perPage: 1);
            Assert.AreEqual(1, response.Data.page);
            Assert.AreEqual(VALID_TOTAL_ALLOWANCE, response.Data.limits[1].total_allowance);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion


        #region Create

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_IdentityId_Fails()
        {
            Client.Account.Identity.Limit.Create(null, VALID_SERVICE, VALID_TOTAL_ALLOWANCE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_IdentityId_Fails()
        {
            Client.Account.Identity.Limit.Create("", VALID_SERVICE, VALID_TOTAL_ALLOWANCE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Invalid_IdentityId_Fails()
        {
            Client.Account.Identity.Limit.Create("invalid ID", VALID_SERVICE, VALID_TOTAL_ALLOWANCE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Service_Fails()
        {
            Client.Account.Identity.Limit.Create(VALID_IDENTITY, null, VALID_TOTAL_ALLOWANCE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Service_Fails()
        {
            Client.Account.Identity.Limit.Create(VALID_IDENTITY, "", VALID_TOTAL_ALLOWANCE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Negative_Allowance_Fails()
        {
            Client.Account.Identity.Limit.Create(VALID_IDENTITY, VALID_SERVICE, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Zero_Allowance_Fails()
        {
            Client.Account.Identity.Limit.Create(VALID_IDENTITY, VALID_SERVICE, 0);
        }

        [TestMethod]
        public void Create_Succeeds()
        {
            var response = Client.Account.Identity.Limit.Create(VALID_IDENTITY, VALID_SERVICE, VALID_TOTAL_ALLOWANCE);
            Assert.AreEqual(VALID_TOTAL_ALLOWANCE, response.Data.total_allowance);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        #endregion

        #region Update

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_Null_IdentityId_Fails()
        {
            Client.Account.Identity.Limit.Update(null, VALID_SERVICE, VALID_TOTAL_ALLOWANCE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_IdentityId_Fails()
        {
            Client.Account.Identity.Limit.Update("", VALID_SERVICE, VALID_TOTAL_ALLOWANCE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Invalid_IdentityId_Fails()
        {
            Client.Account.Identity.Limit.Update("invalid ID", VALID_SERVICE, VALID_TOTAL_ALLOWANCE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_Null_Service_Fails()
        {
            Client.Account.Identity.Limit.Update(VALID_IDENTITY, null, VALID_TOTAL_ALLOWANCE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Empty_Service_Fails()
        {
            Client.Account.Identity.Limit.Update(VALID_IDENTITY, "", VALID_TOTAL_ALLOWANCE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Negative_Allowance_Fails()
        {
            Client.Account.Identity.Limit.Update(VALID_IDENTITY, VALID_SERVICE, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_Zero_Allowance_Fails()
        {
            Client.Account.Identity.Limit.Update(VALID_IDENTITY, VALID_SERVICE, 0);
        }

        [TestMethod]
        public void Update_Succeeds()
        {
            var response = Client.Account.Identity.Limit.Update(VALID_IDENTITY, VALID_SERVICE, 200000);
            Assert.AreEqual(200000, response.Data.total_allowance);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Delete

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_Null_IdentityId_Fails()
        {
            Client.Account.Identity.Limit.Delete(null, VALID_SERVICE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Empty_IdentityId_Fails()
        {
            Client.Account.Identity.Limit.Delete("", VALID_SERVICE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Invalid_IdentityId_Fails()
        {
            Client.Account.Identity.Limit.Delete("invalid ID", VALID_SERVICE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_Null_Service_Fails()
        {
            Client.Account.Identity.Limit.Delete(VALID_IDENTITY, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Empty_Service_Fails()
        {
            Client.Account.Identity.Limit.Delete(VALID_IDENTITY, "");
        }

        [TestMethod]
        public void Delete_Succeeds()
        {
            var response = Client.Account.Identity.Limit.Delete(VALID_IDENTITY, VALID_SERVICE);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

    }
}
