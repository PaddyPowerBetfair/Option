using System;
using Xunit;


namespace Option.Tests.Unit
{
    public class OptionFactoryTests
    {

        #region OptionFactory.Some

        [Fact]
        public void OptionFactory_SomeCalledWithString_StringAndValueShouldBeEquals()
        {
            var some = Option.Some("test");

            Assert.True(some.HasValue);
            Assert.Equal("test", some.Value);
        }

        [Fact]
        public void OptionFactory_SomeCalledWithEmptyString_ValueShouldBeEmptyString()
        {
            var some = Option.Some(string.Empty);

            Assert.True(some.HasValue);
            Assert.Equal(string.Empty, some.Value);
        }

        [Fact]
        public void OptionFactory_SomeCalledWithObject_ObjectAndValueShouldBeEquals()
        {
            var o = new object();
            var some = Option.Some(o);

            Assert.True(some.HasValue);
            Assert.Equal(o, some.Value);
        }

        #endregion

        #region OptionFactory.None

        [Fact]
        public void OptionFactory_NoneCalledWithStringType_IsNoneShouldReturnTrue()
        {
            var none = Option.None<string>();

            Assert.True(none.IsNone);
        }

        [Fact]
        public void OptionFactory_NoneCalledWithObjectType_IsNoneShouldReturnTrue()
        {
            var none = Option.None<object>();

            Assert.True(none.IsNone);
        }

        #endregion

        #region OptionFactory.Try

        [Fact]
        public void OptionFactory_TryCalledWithLambdaReturningString_ValueAndStringShouldBeEquals()
        {
            var @try = Option.Try(() => "test");

            Assert.True(@try.HasValue);
            Assert.Equal("test", @try.Value);
        }

        [Fact]
        public void OptionFactory_TryCalledWithLambdaReturningEmptyString_ValueAndStringShouldBeEquals()
        {
            var @try = Option.Try(() => string.Empty);

            Assert.True(@try.HasValue);
            Assert.Equal(string.Empty, @try.Value);
        }

        [Fact]
        public void OptionFactory_TryCalledWithLambdaReturningObject_ValueAndObjectShouldBeEquals()
        {
            var o = new object();
            var @try = Option.Try(() => o);

            Assert.True(@try.HasValue);
            Assert.Equal(o, @try.Value);
        }

        [Fact]
        public void OptionFactory_TryCallThrowsException_IsNoneShouldBeTrue()
        {
            var @try = Option.Try(GetException);

            Assert.True(@try.IsNone);
        }

        #endregion

        #region OptionFactory.From

        [Fact]
        public void OptionFactory_FromCalledWithString_ValueAndStringShouldBeEquals()
        {
            var from = Option.From("test");

            Assert.True(from.HasValue);
            Assert.Equal("test", from.Value);
        }

        [Fact]
        public void OptionFactory_FromCalledWithEmptyString_ValueShouldBeEmptyString()
        {
            var from = Option.From(string.Empty);

            Assert.True(from.HasValue);
            Assert.Equal(string.Empty, from.Value);
        }

        [Fact]
        public void OptionFactory_FromCalledWithObject_ValueAndObjectShouldBeEquals()
        {
            var o = new object();
            var from = Option.From(o);

            Assert.True(from.HasValue);
            Assert.Equal(o, from.Value);
        }

        [Fact]
        public void OptionFactory_FromCalledWithNull_IsNoneShouldBeTrue()
        {
            var from = Option.From((string) null);
            Assert.True(from.IsNone);
        }

        #endregion

        #region Helpers

        private static string GetException()
        {
            throw new Exception();
        }

        #endregion
    }
}
