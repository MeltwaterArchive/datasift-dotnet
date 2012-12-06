using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;

namespace datasift_tests {
    [TestClass]
    public class Test_JsonDots {
        const string referenceJsonWithDots =
            "{\"start\":\"Tue, 04 Dec 2012 09:35:00 +0000\",\"end\":\"Tue, 04 Dec 2012 10:35:00 +0000\",\"streams\":{\"cfc973789e670fe91aceb4b91cbae4db\":{\"licenses\":{\"gender\":3754,\"interaction\":33134,\"klout.score\":30271,\"language\":31022,\"links\":8590,\"salience.sentiment\":30948,\"trends\":3612,\"twitter\":33134},\"seconds\":150},\"947b690ec9dca525fb8724645e088d79\":{\"licenses\":[],\"seconds\":136}}}";

        private datasift.JSONdn decoder;

        [TestInitialize]
        public void InitTest() {
            decoder = new datasift.JSONdn(referenceJsonWithDots);
        }

        [TestMethod]
        public void Test_JsonDot1() {
            Assert.IsTrue(decoder.has("start"));
            Assert.IsFalse(decoder.has("zzbobbuilderxx"));
            Assert.IsTrue(decoder.has("streams.cfc973789e670fe91aceb4b91cbae4db.licenses.gender"));
        }

        [TestMethod]
        [Ignore] // Test to show the original problem, can't fix, so putting in alternative interface.
        public void Test_JsonDot_OriginalProblem() {
            Assert.IsTrue(decoder.has("streams.cfc973789e670fe91aceb4b91cbae4db.licenses.klout.score"));
            Assert.AreEqual(decoder.getLongVal("streams.cfc973789e670fe91aceb4b91cbae4db.licenses.klout.score"), 30271);
        }

        [TestMethod]
        public void Test_JsonDot3() {
            Assert.IsTrue(decoder.has("streams.cfc973789e670fe91aceb4b91cbae4db.licenses"));
            Assert.IsTrue(decoder.has("streams.cfc973789e670fe91aceb4b91cbae4db.licenses."+ datasift.JSONdn.EscapeDotsInKey("klout.score")));
            Assert.AreEqual(decoder.getStringVal("streams.cfc973789e670fe91aceb4b91cbae4db.licenses." + datasift.JSONdn.EscapeDotsInKey( "klout.score" )), "30271");
            Assert.AreEqual(decoder.getLongVal("streams.cfc973789e670fe91aceb4b91cbae4db.licenses." +datasift.JSONdn.EscapeDotsInKey("klout.score" )), 30271);
        }
    }

    public class JsonTestConst
    {
        public const string jsonWithDots =
            "{\"key1.0\": {\"a.key\": \"a.value\", \"b.key\": { \"X\":[\"y\",\"z\"], \".net\": \"C♯\" }, \"b\":{\"key\": \"v\"}}}";
    }

    [TestClass]
    public class Test_JsonDots_new {
        private datasift.JSONdn decoder;

        [TestInitialize]
        public void InitTest() {
            decoder = new datasift.JSONdn(JsonTestConst.jsonWithDots);
        }

        [TestMethod]
        [Ignore] //don't change to do this
        public void Test_JsonDotArbLate()
        {
            //change to arbitrary dessission in interperating ambiguouse grama
            Assert.IsTrue(decoder.has("key1.0.b.key..net"));
            Assert.IsTrue(decoder.has("key1.0.b.key"));
        }

        [TestMethod]
        public void Test_JsonBackSlashDot()
        {
            //does this make sence, to do it this way?
            Assert.IsTrue(decoder.has(@"key1\.0.b\.key.\.net"));
            Assert.IsTrue(decoder.has(@"key1\.0.b.key" ));
            Assert.AreEqual("C♯",decoder.getStringVal(@"key1\.0.b\.key.\.net"));
        }

        [TestMethod]
        [Ignore] //don't change to do this, as will break things.
        public void Test_JsonBackSlashDotAlt_BadIdea()
        {
            //seperator is \.
            //does this make sence, to do it this way?
            Assert.IsTrue(decoder.has(@"key1.0\.b.key\..net"));
            Assert.IsTrue(decoder.has(@"key1.0\.b\.key"));
            Assert.AreEqual("C♯",decoder.getStringVal(@"key1.0\.b.key\..net"));
        }
    }
}
