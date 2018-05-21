using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Option.Tests.Unit
{
    [TestClass]
    public class OptionFactoryTests
    {
        [TestMethod]
        public void OptionCreateSomeString()
        {
            var some = Option.Some("test");

            Assert.IsTrue(some.HasValue);
            Assert.AreEqual("test", some.Value);
        }

        [TestMethod]
        public void OptionCreateSomeEmptyString()
        {
            var some = Option.Some(string.Empty);

            Assert.IsTrue(some.HasValue);
            Assert.AreEqual(string.Empty, some.Value);
        }

        [TestMethod]
        public void OptionCreateSomeObject()
        {
            var o = new object();
            var some = Option.Some(o);

            Assert.IsTrue(some.HasValue);
            Assert.AreEqual(o, some.Value);
        }

        [TestMethod]
        public void OptionCreateNoneString()
        {
            var none = Option.None<string>();

            Assert.IsTrue(none.IsNone);
        }

        [TestMethod]
        public void OptionCreateNoneObject()
        {
            var none = Option.None<object>();

            Assert.IsTrue(none.IsNone);
        }

        [TestMethod]
        public void OptionTrySomeString()
        {
            var @try = Option.Try(() => "test");

            Assert.IsTrue(@try.HasValue);
            Assert.AreEqual("test", @try.Value);
        }

        [TestMethod]
        public void OptionTrySomeEmptyString()
        {
            var @try = Option.Try(() => string.Empty);

            Assert.IsTrue(@try.HasValue);
            Assert.AreEqual(string.Empty, @try.Value);
        }

        [TestMethod]
        public void OptionTrySomeObject()
        {
            var o = new object();
            var @try = Option.Try(() => o);

            Assert.IsTrue(@try.HasValue);
            Assert.AreEqual(o, @try.Value);
        }

        [TestMethod]
        public void OptionTryNoneException()
        {
            var @try = Option.Try(GetException);

            Assert.IsTrue(@try.IsNone);
        }

        [TestMethod]
        public void OptionFromString()
        {
            var from = Option.From("test");

            Assert.IsTrue(from.HasValue);
            Assert.AreEqual("test", from.Value);
        }

        [TestMethod]
        public void OptionFromStringEmpty()
        {
            var from = Option.From(string.Empty);

            Assert.IsTrue(from.HasValue);
            Assert.AreEqual(string.Empty, from.Value);
        }

        [TestMethod]
        public void OptionFromObject()
        {
            var o = new object();
            var from = Option.From(o);

            Assert.IsTrue(from.HasValue);
            Assert.AreEqual(o, from.Value);
        }

        [TestMethod]
        public void OptionFromNull()
        {
            var from = Option.From((string) null);
            Assert.IsTrue(from.IsNone);
        }

        private static string GetException()
        {
            throw new Exception();
        }
    }
}
