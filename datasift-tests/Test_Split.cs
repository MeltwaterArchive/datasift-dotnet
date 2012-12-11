using System;
using NUnit.Framework;

namespace datasift_tests
{
    [TestFixture]
    public class Test_Split
    {
        [TestCase(@"hhh\.ggg",             new string[] { @"hhh.ggg"})]
        [TestCase(@"hhh\\.ggg",            new string[] { @"hhh\\",@"ggg"})]
        [TestCase(@"hhh\\\.ggg",           new string[] { @"hhh\\.ggg"})]
        [TestCase(@"nnn\\\\.ggg",          new string[] { @"nnn\\\\", @"ggg"})]
        [TestCase(@"hhh\\\\.ggg",          new string[] { @"hhh\\\\", @"ggg" })]
        [TestCase(@"hhh\..ggg",            new string[] { @"hhh.", @"ggg" })]
        [TestCase(@"hhh\.\.ggg",           new string[] { @"hhh..ggg" })]
        [TestCase(@"hhh\\\.",              new string[] { @"hhh\\." })]
        [TestCase(@"\\\.hhh",              new string[] { @"\\.hhh" })]
        [TestCase(@"aaa\nbbb\.g.gg",       new string[] { @"aaa\nbbb.g", @"gg" })]
        [TestCase(@"key1\.0.b\.key.\.net", new string[] { @"key1.0", @"b.key", @".net" })]
        public void dot_split(string input, string[] expected)
        {
            Assert.That(
                datasift.JSONdn._SplitAndUnescape(input),
                Is.EqualTo( expected ));
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
        [TestCase(@"hello\nworld")]
        [TestCase(@"h.ello\nworl.d")]
        public void dot_split_BackwardCompatible(string input)
        {
            Assert.That(
                datasift.JSONdn._SplitAndUnescape(input),
                Is.EqualTo(input.Split('.')));
        }
    }

    [TestFixture]
    public class Test_DotEscape {
        [TestCase("", "")]
        [TestCase(@".", @"\.")]
        [TestCase(@"a.b", @"a\.b")]
        [TestCase(@"a\n.b", @"a\n\.b")]
        public void dot_Escape(string input, string Expected)
        {
            Assert.That(
                datasift.JSONdn.EscapeDotsInKey(input),
                Is.EqualTo(Expected));
        }
    }
}


