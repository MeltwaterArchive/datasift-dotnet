using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace datasift_test2
{
    [TestFixture]
    class getPostDatacs
    {
        [TestCaseSource("TestCases")]
        public void Test_AbstractApiClient_getPostData(
            Dictionary<string, string> input, 
            string expected)
        {
            var t = new datasift.AbstractApiClient("", "", "", "");
            Assert.That(t.getPostData(input), Is.EqualTo(expected));
        }

        static object[] TestCases =
        {
            new object[] {
                new Dictionary<string, string>(){{"a","1"}, {"b","2"}},
                "a=1&b=2"
            },
            new object[] {
                new Dictionary<string, string>(){{"a","1"}, {"b","2"}, {"hello","world"}},
                "a=1&b=2&hello=world"
            },
            //one arg
            new object[] {
                new Dictionary<string, string>(){{"hello","world"}},
                "hello=world"
            },
#if false
            //zero args
            new object[] {
                new Dictionary<string, string>(),
                ""
            },
#endif
        };
    }
}
