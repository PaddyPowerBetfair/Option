using System;
using Xunit;


namespace Option.Tests.Unit
{
    public class OptionFactoryTests
    {
        [Fact]
        public void OptionCreateSomeString()
        {
            var some = Option.Some("test");

            Assert.True(some.HasValue);
            Assert.Equal("test", some.Value);
        }

        [Fact]
        public void OptionCreateSomeEmptyString()
        {
            var some = Option.Some(string.Empty);

            Assert.True(some.HasValue);
            Assert.Equal(string.Empty, some.Value);
        }

        [Fact]
        public void OptionCreateSomeObject()
        {
            var o = new object();
            var some = Option.Some(o);

            Assert.True(some.HasValue);
            Assert.Equal(o, some.Value);
        }

        [Fact]
        public void OptionCreateNoneString()
        {
            var none = Option.None<string>();

            Assert.True(none.IsNone);
        }

        [Fact]
        public void OptionCreateNoneObject()
        {
            var none = Option.None<object>();

            Assert.True(none.IsNone);
        }

        [Fact]
        public void OptionTrySomeString()
        {
            var @try = Option.Try(() => "test");

            Assert.True(@try.HasValue);
            Assert.Equal("test", @try.Value);
        }

        [Fact]
        public void OptionTrySomeEmptyString()
        {
            var @try = Option.Try(() => string.Empty);

            Assert.True(@try.HasValue);
            Assert.Equal(string.Empty, @try.Value);
        }

        [Fact]
        public void OptionTrySomeObject()
        {
            var o = new object();
            var @try = Option.Try(() => o);

            Assert.True(@try.HasValue);
            Assert.Equal(o, @try.Value);
        }

        [Fact]
        public void OptionTryNoneException()
        {
            var @try = Option.Try(GetException);

            Assert.True(@try.IsNone);
        }

        [Fact]
        public void OptionFromString()
        {
            var from = Option.From("test");

            Assert.True(from.HasValue);
            Assert.Equal("test", from.Value);
        }

        [Fact]
        public void OptionFromStringEmpty()
        {
            var from = Option.From(string.Empty);

            Assert.True(from.HasValue);
            Assert.Equal(string.Empty, from.Value);
        }

        [Fact]
        public void OptionFromObject()
        {
            var o = new object();
            var from = Option.From(o);

            Assert.True(from.HasValue);
            Assert.Equal(o, from.Value);
        }

        [Fact]
        public void OptionFromNull()
        {
            var from = Option.From((string) null);
            Assert.True(from.IsNone);
        }

        private static string GetException()
        {
            throw new Exception();
        }
    }
}
