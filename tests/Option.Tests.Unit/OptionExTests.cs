using Option.Tests.Unit.Helpers;
using Xunit;

namespace Option.Tests.Unit
{
    
    public class OptionExTests
    {
        [Fact]
        public void OptionExToNullable()
        {
            var counter = Option.Some(new Counter()).ToNullable();

            Assert.True(counter.HasValue);
            Assert.Equal(0, counter.Value.Item);
            Assert.Equal(0, counter.Value.Count);
        }

        
        [Fact]
        public void OptionExToNullableWithItems()
        {
            var counter = Option.Some(new Counter{Count = 1, Item = 2}).ToNullable();

            Assert.True(counter.HasValue);
            Assert.Equal(2, counter.Value.Item);
            Assert.Equal(1, counter.Value.Count);
        }

        [Fact]
        public void OptionExSelectSome()
        {
            var option = Option.Some("test")
                .Select(item => item.Length);

            Assert.True(option.HasValue);
            Assert.Equal(4, option.Value);
        }

        [Fact]
        public void OptionExSelectNone()
        {
            var option = Option.None<string>()
                .Select(item => item.Length);

            Assert.True(option.IsNone);
        }
    }

}
