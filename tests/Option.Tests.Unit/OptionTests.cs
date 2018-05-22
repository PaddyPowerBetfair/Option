using Xunit;

namespace Option.Tests.Unit
{
   
    public class OptionTests
    {
        [Fact]
        public void OptionValueOrSomeStringTest()
        {
            var option = Option.Some("test");
            var check = option.ValueOr(string.Empty);

            Assert.Equal("test", check);

        }

        [Fact]
        public void OptionValueOrNoneStringTest()
        {
            var option = Option.None<string>();
            var check = option.ValueOr("test");

            Assert.Equal("test", check);
        }

        [Fact]
        public void OptionEqualsBothStrings()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.Some("test");

            Assert.True(firstOption.Equals(secondOption));
        }

        [Fact]
        public void OptionEqualsOneStrings()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.None<string>();

            Assert.False(firstOption.Equals(secondOption));
        }

        [Fact]
        public void OptionEqualsStringsDifferent()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.Some("test2");

            Assert.False(firstOption.Equals(secondOption));
        }

        [Fact]
        public void OptionEqualsInt()
        {
            var firstOption = Option.Some(10);
            var secondOption = Option.Some(10);

            Assert.True(firstOption.Equals(secondOption));
        }

        [Fact]
        public void OptionEqualsIntDifferent()
        {
            var firstOption = Option.Some(10);
            var secondOption = Option.Some(11);

            Assert.False(firstOption.Equals(secondOption));
        }

        [Fact]
        public void OptionEqualsOperator()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.Some("test");

            Assert.True(firstOption == secondOption);
        }

        [Fact]
        public void OptionEqualsOneStringsOperator()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.None<string>();

            Assert.True(firstOption != secondOption);
        }

        [Fact]
        public void OptionEqualsStringsDifferentOperator()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.Some("test2");

            Assert.True(firstOption != secondOption);
        }

        [Fact]
        public void OptionEqualsIntOperator()
        {
            var firstOption = Option.Some(10);
            var secondOption = Option.Some(10);

            Assert.True(firstOption == secondOption);
        }

        [Fact]
        public void OptionEqualsIntDifferentOperator()
        {
            var firstOption = Option.Some(10);
            var secondOption = Option.Some(11);

            Assert.True(firstOption != secondOption);
        }

        [Fact]
        public void OptionGetHashCodeInt()
        {
            var option = Option.Some(1).GetHashCode();

            Assert.IsType<int>(option);
        }

        [Fact]
        public void OptionGetHashCodeString()
        {
            var option = Option.Some("test").GetHashCode();

            Assert.IsType<int>(option);
        }

        [Fact]
        public void OptionToStringString()
        {
            var option = Option.Some("test").ToString();

            Assert.Equal("Some<String>(test)", option);
        }

        [Fact]
        public void OptionToStringInt()
        {
            var option = Option.Some(10).ToString();

            Assert.Equal("Some<Int32>(10)", option);
        }

    }
}
