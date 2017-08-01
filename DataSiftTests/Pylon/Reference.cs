using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace DataSiftTests.Pylon
{
    [TestClass]
    public class Reference : TestBase
    {
        public const string VALID_SERVICE = "linkedin";
        public const string VALID_SLUG = "functions";

        #region Get all
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Get_All_Service_Null_Fails()
        {
            Client.Pylon.Reference.Get(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_All_Service_Empty_Fails()
        {
            Client.Pylon.Reference.Get("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_All_Page_Is_Less_Than_One_Fails()
        {
            Client.Pylon.Reference.Get(VALID_SERVICE, page: 0);
        }

        [TestMethod]
        public void Get_All_Page_Succeeds()
        {
            var response = Client.Pylon.Reference.Get(VALID_SERVICE, page: 2);
            Assert.AreEqual(2, response.Data.page);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_All_Per_Page_Is_Less_Than_One_Fails()
        {
            Client.Pylon.Reference.Get(VALID_SERVICE, perPage: 0);
        }

        [TestMethod]
        public void Get_All_PerPage_Succeeds()
        {
            var response = Client.Pylon.Reference.Get(VALID_SERVICE, page: 1, perPage: 5);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(5, response.Data.data.Count);
        }

        [TestMethod]
        public void Get_All_Succeeds()
        {
            var response = Client.Pylon.Reference.Get(VALID_SERVICE);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(15, response.Data.data.Count);
        }

        #endregion

        #region Get one

        [ExpectedException(typeof(ArgumentNullException))]
        public void Get_One_Slug_Null_Fails()
        {
            Client.Pylon.Reference.Get(VALID_SERVICE, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_One_Slug_Empty_Fails()
        {
            Client.Pylon.Reference.Get(VALID_SERVICE, "");
        }

        [TestMethod]
        public void Get_One_Succeeds()
        {
            var response = Client.Pylon.Reference.Get(VALID_SERVICE, VALID_SLUG);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("accounting", response.Data.values[0].value);
        }

        #endregion


    }
}
