using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Option.Tests.Unit
{
    [TestClass]
    public class OptionTests
    {
        [TestMethod]
        public void OptionValueOrSomeStringTest()
        {
            var option = Option.Some("test");
            var check = option.ValueOr(string.Empty);

            Assert.AreEqual("test", check);

        }

        [TestMethod]
        public void OptionValueOrNoneStringTest()
        {
            var option = Option.None<string>();
            var check = option.ValueOr("test");

            Assert.AreEqual("test", check);
        }

        [TestMethod]
        public void OptionEqualsBothStrings()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.Some("test");

            Assert.IsTrue(firstOption.Equals(secondOption));
        }

        [TestMethod]
        public void OptionEqualsOneStrings()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.None<string>();

            Assert.IsFalse(firstOption.Equals(secondOption));
        }

        [TestMethod]
        public void OptionEqualsStringsDifferent()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.Some("test2");

            Assert.IsFalse(firstOption.Equals(secondOption));
        }

        [TestMethod]
        public void OptionEqualsInt()
        {
            var firstOption = Option.Some(10);
            var secondOption = Option.Some(10);

            Assert.IsTrue(firstOption.Equals(secondOption));
        }

        [TestMethod]
        public void OptionEqualsIntDifferent()
        {
            var firstOption = Option.Some(10);
            var secondOption = Option.Some(11);

            Assert.IsFalse(firstOption.Equals(secondOption));
        }

        [TestMethod]
        public void OptionEqualsOperator()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.Some("test");

            Assert.IsTrue(firstOption == secondOption);
        }

        [TestMethod]
        public void OptionEqualsOneStringsOperator()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.None<string>();

            Assert.IsTrue(firstOption != secondOption);
        }

        [TestMethod]
        public void OptionEqualsStringsDifferentOperator()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.Some("test2");

            Assert.IsTrue(firstOption != secondOption);
        }

        [TestMethod]
        public void OptionEqualsIntOperator()
        {
            var firstOption = Option.Some(10);
            var secondOption = Option.Some(10);

            Assert.IsTrue(firstOption == secondOption);
        }

        [TestMethod]
        public void OptionEqualsIntDifferentOperator()
        {
            var firstOption = Option.Some(10);
            var secondOption = Option.Some(11);

            Assert.IsTrue(firstOption != secondOption);
        }

        [TestMethod]
        public void OptionGetHashCodeInt()
        {
            var option = Option.Some(1).GetHashCode();

            Assert.IsInstanceOfType(option, typeof(int));
        }

        [TestMethod]
        public void OptionGetHashCodeString()
        {
            var option = Option.Some("test").GetHashCode();

            Assert.IsInstanceOfType(option, typeof(int));
        }

        [TestMethod]
        public void OptionToStringString()
        {
            var option = Option.Some("test").ToString();

            Assert.AreEqual("Some<String>(test)", option);
        }

        [TestMethod]
        public void OptionToStringInt()
        {
            var option = Option.Some(10).ToString();

            Assert.AreEqual("Some<Int32>(10)", option);
        }

    }
}
