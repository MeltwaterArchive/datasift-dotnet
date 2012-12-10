using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace datasift_tests
{
    [TestFixture]
    class DotSplit_Speed
    {
        const string someJson = "{\"start\":\"Tue, 04 Dec 2012 09:35:00 +0000\",\"end\":\"Tue, 04 Dec 2012 10:35:00 +0000\",\"streams\":{\"cfc973789e670fe91aceb4b91cbae4db\":{\"licenses\":{\"gender\":3754,\"interaction\":33134,\"klout-score\":30271,\"language\":31022,\"links\":8590,\"salience-sentiment\":30948,\"trends\":3612,\"twitter\":33134},\"seconds\":150},\"947b690ec9dca525fb8724645e088d79\":{\"licenses\":[],\"seconds\":136}}}";

        datasift.JSONdn json;

        [SetUp]
        public void setup()
        {
            json = new datasift.JSONdn(someJson);
        }

        [Test]
        public void speed_dotSplitNew()
        {
            var start_time = System.DateTime.Now;        
            for (var i = 0; i<1000000;i++)
            {
                var t=json.resolveString("streams.cfc973789e670fe91aceb4b91cbae4db.licenses.gender");
            }
            var finish_time = System.DateTime.Now;

            var duration = finish_time - start_time;
            System.Console.WriteLine("new resolver duration=" + duration.TotalSeconds);
        }

        [Test]
        public void speed_dotSplitOld()
        {
            var start_time = System.DateTime.Now;
            for (var i = 0; i < 1000000; i++)
            {
                var t = json.resolveString_old("streams.cfc973789e670fe91aceb4b91cbae4db.licenses.gender");
            }
            var finish_time = System.DateTime.Now;

            var duration = finish_time - start_time;
            System.Console.WriteLine("old resolver duration=" + duration.TotalSeconds);
        }
    }
}
