using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using DataSift.Enum;

namespace DataSiftTests
{
    [TestClass]
    public class List : TestBase
    {
        private const string VALID_LIST_ID = "0x08fa577f_7528_44ce_9671_692f2e3fd25e";
        private const string VALID_NAME = "Test text list";
        private int[] VALID_INT_LIST = new int[] { 1, 2, 3, 4, 5 };

        #region Get

        [TestMethod]
        public void Get_Succeeds()
        {
            var response = Client.List.Get();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Create

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Null_Name_Fails()
        {
            Client.List.Create(ListType.Integer, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Empty_Name_Fails()
        {
            Client.List.Create(ListType.Integer, "");
        }

        [TestMethod]
        public void Create_Succeeds()
        {
            var response = Client.List.Create(ListType.Text, VALID_NAME);
            Assert.AreEqual(VALID_LIST_ID, response.Data.id);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        #endregion

        #region Delete

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_Null_Id_Fails()
        {
            Client.List.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Empty_Id_Fails()
        {
            Client.List.Delete("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Bad_Format_Id_Fails()
        {
            Client.List.Delete("id");
        }

        [TestMethod]
        public void Delete_Correct_Args_Succeeds()
        {
            var response = Client.List.Delete(VALID_LIST_ID);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region Add

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_Null_Id_Fails()
        {
            Client.List.Add(null, VALID_INT_LIST);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_Empty_Id_Fails()
        {
            Client.List.Add("", VALID_INT_LIST);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_Bad_Format_Id_Fails()
        {
            Client.List.Add("list", VALID_INT_LIST);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_Bad_Type_Fails()
        {
            Client.List.Add(VALID_LIST_ID, new float[] { 1f, 2f, 3f, 4f, 5f });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_No_Items_Fails()
        {
            Client.List.Add(VALID_LIST_ID, new int[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_More_Than_Thousand_Fails()
        {
            var items = new int[1001];

            for (int i = 0; i <= 1000; i++)
            {
                items[i] = i;
            }

            Client.List.Add(VALID_LIST_ID, items);
        }

        [TestMethod]
        public void Add_Correct_Args_Succeeds()
        {
            var response = Client.List.Add(VALID_LIST_ID, VALID_INT_LIST);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region Remove

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_Null_Id_Fails()
        {
            Client.List.Remove(null, VALID_INT_LIST);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Remove_Empty_Id_Fails()
        {
            Client.List.Remove("", VALID_INT_LIST);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Remove_Bad_Format_Id_Fails()
        {
            Client.List.Remove("list", VALID_INT_LIST);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Remove_Bad_Type_Fails()
        {
            Client.List.Remove(VALID_LIST_ID, new float[] { 1f, 2f, 3f, 4f, 5f });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Remove_No_Items_Fails()
        {
            Client.List.Remove(VALID_LIST_ID, new int[] { });
        }

        [TestMethod]
        public void Remove_Correct_Args_Succeeds()
        {
            var response = Client.List.Remove(VALID_LIST_ID, VALID_INT_LIST);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region Exists

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Exists_Null_Id_Fails()
        {
            Client.List.Exists(null, VALID_INT_LIST);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Exists_Empty_Id_Fails()
        {
            Client.List.Exists("", VALID_INT_LIST);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Exists_Bad_Format_Id_Fails()
        {
            Client.List.Exists("list", VALID_INT_LIST);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Exists_Bad_Type_Fails()
        {
            Client.List.Exists(VALID_LIST_ID, new float[] { 1f, 2f, 3f, 4f, 5f });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Exists_No_Items_Fails()
        {
            Client.List.Exists(VALID_LIST_ID, new int[] { });
        }

        [TestMethod]
        public void Exists_Correct_Args_Succeeds()
        {
            var response = Client.List.Exists(VALID_LIST_ID, new string[] { "keyword1", "keyword2", "keyword3" });
            Assert.AreEqual(true, response.Data.keyword1);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Replace Start

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceStart_Null_Id_Fails()
        {
            Client.List.ReplaceStart(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReplaceStart_Empty_Id_Fails()
        {
            Client.List.ReplaceStart("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReplaceStart_Bad_Format_Id_Fails()
        {
            Client.List.ReplaceStart("replace");
        }

        [TestMethod]
        public void ReplaceStart_Correct_Args_Succeeds()
        {
            var response = Client.List.ReplaceStart(VALID_LIST_ID);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Replace Abort

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceAbort_Null_Id_Fails()
        {
            Client.List.ReplaceAbort(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReplaceAbort_Empty_Id_Fails()
        {
            Client.List.ReplaceAbort("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReplaceAbort_Bad_Format_Id_Fails()
        {
            Client.List.ReplaceAbort("replace");
        }

        [TestMethod]
        public void ReplaceAbort_Correct_Args_Succeeds()
        {
            var response = Client.List.ReplaceAbort(VALID_LIST_ID);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region Replace Commit

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceCommit_Null_Id_Fails()
        {
            Client.List.ReplaceCommit(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReplaceCommit_Empty_Id_Fails()
        {
            Client.List.ReplaceCommit("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReplaceCommit_Bad_Format_Id_Fails()
        {
            Client.List.ReplaceCommit("replace");
        }

        [TestMethod]
        public void ReplaceCommit_Correct_Args_Succeeds()
        {
            var response = Client.List.ReplaceCommit(VALID_LIST_ID);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region Replace Add

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceAdd_Null_Id_Fails()
        {
            Client.List.ReplaceAdd(null, VALID_INT_LIST);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReplaceAdd_Empty_Id_Fails()
        {
            Client.List.ReplaceAdd("", VALID_INT_LIST);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReplaceAdd_Bad_Format_Id_Fails()
        {
            Client.List.ReplaceAdd("list", VALID_INT_LIST);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReplaceAdd_Bad_Type_Fails()
        {
            Client.List.ReplaceAdd(VALID_LIST_ID, new float[] { 1f, 2f, 3f, 4f, 5f });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReplaceAdd_No_Items_Fails()
        {
            Client.List.ReplaceAdd(VALID_LIST_ID, new int[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReplaceAdd_More_Than_Thousand_Fails()
        {
            var items = new int[1001];

            for (int i = 0; i <= 1000; i++)
            {
                items[i] = i;
            }

            Client.List.ReplaceAdd(VALID_LIST_ID, items);
        }

        [TestMethod]
        public void ReplaceAdd_Correct_Args_Succeeds()
        {
            var response = Client.List.ReplaceAdd(VALID_LIST_ID, VALID_INT_LIST);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion
    }
}
