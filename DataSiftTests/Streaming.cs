using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataSift.Streaming;
using System.Threading;

namespace DataSiftTests
{
    [TestClass]
    public class Streaming : TestBase
    {
        AutoResetEvent _TestTrigger;

        private const string VALID_STREAM_HASH = "b09z345fe2f1fed748c12268fd473662";

        #region Subscribe

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Subscribe_With_Null_Hash_Fails()
        {
            var stream = Client.Connect();
            stream.Subscribe(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Subscribe_With_Empty_Hash_Fails()
        {
            var stream = Client.Connect();
            stream.Subscribe("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Subscribe_With_Invalid_Hash_Fails()
        {
            var stream = Client.Connect();
            stream.Subscribe("hash");
        }

        [TestMethod]
        public void Subscribe_Global_With_Valid_Hash_Succeeds()
        {
            var stream = Client.Connect(); 
            
            this._TestTrigger = new AutoResetEvent(false);

            stream.OnSubscribed += delegate(string hash)
            {
                Assert.AreEqual(VALID_STREAM_HASH, hash); 
                this._TestTrigger.Set();
            };

            stream.Subscribe(VALID_STREAM_HASH); 
            this._TestTrigger.WaitOne();
        }

        [TestMethod]
        public void Subscribe_Local_With_Valid_Hash_Succeeds()
        {
            var stream = Client.Connect();
            this._TestTrigger = new AutoResetEvent(false);

            DataSift.Streaming.DataSiftStream.OnSubscribedHandler onSubscribed = (hash) =>
            {
                Assert.AreEqual(VALID_STREAM_HASH, hash);
                this._TestTrigger.Set();
            };

            stream.Subscribe(VALID_STREAM_HASH, subscribedHandler: onSubscribed);
            this._TestTrigger.WaitOne();
        }

        #endregion

        #region Unsubscribe

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Unsubscribe_With_Null_Hash_Fails()
        {
            var stream = Client.Connect();
            stream.Unsubscribe(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Unsubscribe_With_Empty_Hash_Fails()
        {
            var stream = Client.Connect();
            stream.Unsubscribe("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Unsubscribe_With_Invalid_Hash_Fails()
        {
            var stream = Client.Connect();
            stream.Unsubscribe("hash");
        }

        #endregion


        #region OnMessage

        [TestMethod]
        public void Receive_Message_Global()
        {
            var stream = Client.Connect();

            this._TestTrigger = new AutoResetEvent(false);

            stream.OnMessage += delegate(string hash, dynamic message)
            {
                Assert.AreEqual(VALID_STREAM_HASH, hash);
                Assert.AreEqual("Test content", message.interaction.content);
                this._TestTrigger.Set();
            };

            stream.Subscribe(VALID_STREAM_HASH);
            this._TestTrigger.WaitOne();
        }

        [TestMethod]
        public void Connect_Local_Succeeds()
        {
            var stream = Client.Connect();
            this._TestTrigger = new AutoResetEvent(false);

            DataSift.Streaming.DataSiftStream.OnMessageHandler onMessage = (hash, message) =>
            {
                Assert.AreEqual(VALID_STREAM_HASH, hash);
                Assert.AreEqual("Test content", message.interaction.content);
                this._TestTrigger.Set();
            };

            stream.Subscribe(VALID_STREAM_HASH, messageHandler: onMessage);
            this._TestTrigger.WaitOne();
        }

        #endregion

    }
}
