using System;
using NUnit.Framework;

namespace datasift_tests
{
    [TestFixture]
    public class SplitTests
    {
        public string[] Split(string str)
        {
            return datasift.JSONdn._Split(str);
        }

        [Test]
        public void split2()
        {
            Assert.That(
                Split(@"hhh\.ggg"),
                Is.EqualTo(new string[] { @"hhh.ggg" }));
        }
        [Test]
        public void split3()
        {
            Assert.That(
                Split(@"hhh\\.ggg"),
                Is.EqualTo(new string[]{@"hhh\\",@"ggg"} ));
        }
        [Test]
        public void split4()
        {
            Assert.That(
                Split(@"hhh\\\.ggg"),
                Is.EqualTo(new string[] { @"hhh\\.ggg" }));
        }
        [Test]
        public void split5()
        {
            Assert.That(
                Split(@"hhh\\\\.ggg"),
                Is.EqualTo(new string[] { @"hhh\\\\", @"ggg" }));
        }
        [Test]
        public void split6()
        {
            Assert.That(
                Split(@"hhh\..ggg"),
                Is.EqualTo(new string[] { @"hhh.", @"ggg" }));
        }
        [Test]
        public void split7()
        {
            Assert.That(
                Split(@"hhh\.\.ggg"),
                Is.EqualTo(new string[] { @"hhh..ggg" }));
        }
        [Test]
        public void split8()
        {
            Assert.That(
                Split(@"hhh\\\."),
                Is.EqualTo(new string[] { @"hhh\\." }));
        }
        public void split9()
        {
            Assert.That(
                Split(@"\\\.hhh"),
                Is.EqualTo(new string[] { @"\\.hhh" }));
        }

        [Test]
        public void split_otherEscape()
        {
            Assert.That(
                Split(@"aaa\nbbb\.g.gg"),
                Is.EqualTo(new string[] { @"aaa\nbbb.g", @"gg" }));

        }
        [Test]
        public void split()
        {
            Assert.That(
                Split(@"key1\.0.b\.key.\.net"),
                Is.EqualTo(new string[] { @"key1.0", @"b.key", @".net" }));
        }

        [TestCase("")]
        [TestCase(".")]
        [TestCase("hhh..ggg")]
        [TestCase("hhh.ggg")]
        [TestCase(".hello")]
        [TestCase("world.")]
        [TestCase("..hello")]
        [TestCase("world..")]
        [TestCase(@"world\\.")]
        [TestCase(@"\\.world")]
        [TestCase(@"hello\\world")]
        public void split_BackwardCompatible(string s)
        {
            Assert.That(
                Split(s),
                Is.EqualTo(s.Split('.')));
        }

        [TestCase("", "")]
        [TestCase(@".", @"\.")]
        [TestCase(@"a.b", @"a\.b")]
        [TestCase(@"a\n.b", @"a\n\.b")]
        public void dotEscape(string s, string Expected)
        {
            Assert.That(
                datasift.JSONdn.EscapeDotsInKey(s),
                Is.EqualTo(Expected));
        }
    }
}


