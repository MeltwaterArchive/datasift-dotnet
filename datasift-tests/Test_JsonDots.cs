using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;

namespace datasift_tests {
    [TestClass]
    public class Test_JsonDots {
        const string jsonWithDots =
            "{\"start\":\"Tue, 04 Dec 2012 09:35:00 +0000\",\"end\":\"Tue, 04 Dec 2012 10:35:00 +0000\",\"streams\":{\"cfc973789e670fe91aceb4b91cbae4db\":{\"licenses\":{\"gender\":3754,\"interaction\":33134,\"klout.score\":30271,\"language\":31022,\"links\":8590,\"salience.sentiment\":30948,\"trends\":3612,\"twitter\":33134},\"seconds\":150},\"947b690ec9dca525fb8724645e088d79\":{\"licenses\":[],\"seconds\":136}}}";     

        [TestMethod]
        public void Test_JsonDot() {
            var decoder = new datasift.JSONdn(jsonWithDots);
            Assert.IsTrue(decoder.has("start"));
            Assert.IsFalse(decoder.has("zzbobbuilderxx"));
            Assert.IsTrue(decoder.has("streams.cfc973789e670fe91aceb4b91cbae4db.licenses.gender"));
        }

        [TestMethod]
        public void Test_JsonDot2() {
            var decoder = new datasift.JSONdn(jsonWithDots);
            Assert.IsTrue(decoder.has("streams.cfc973789e670fe91aceb4b91cbae4db.licenses.klout.score"));
            //Assert.That(decoder.getLongVal("streams.cfc973789e670fe91aceb4b91cbae4db.licenses.klout.score"), Is.EqualTo(30271));
        }
        
    }
}
