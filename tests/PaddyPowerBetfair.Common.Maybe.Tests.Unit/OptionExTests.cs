using PaddyPowerBetfair.Common.Maybe.Extensions;
using PaddyPowerBetfair.Common.Maybe.Tests.Unit.Helpers;
using Xunit;

namespace PaddyPowerBetfair.Common.Maybe.Tests.Unit
{
    
    public class OptionExTests
    {

        #region OptionExtensions ToNullable
        [Fact]
        public void OptionExtensions_ToNullableCalledOnOptionContainingObject_ObjectShouldBeRetained()
        {
            var counter = Option.Some(new Counter()).ToNullable();

            Assert.True(counter.HasValue);
            Assert.Equal(0, counter.Value.Item);
            Assert.Equal(0, counter.Value.Count);
        }

        
        [Fact]
        public void OptionExtensions_ToNullableCalledOnOptionContainingObject_StateOfObjectShouldBeRetained()
        {
            var counter = Option.Some(new Counter{Count = 1, Item = 2}).ToNullable();

            Assert.True(counter.HasValue);
            Assert.Equal(2, counter.Value.Item);
            Assert.Equal(1, counter.Value.Count);
        }

        #endregion

        #region OptionExtensions Select

        [Fact]
        public void OptionExtensions_SelectSomeCalledWithLambda_ValueAndResultOfLambdaShouldBeEquals()
        {
            var option = Option.Some("test")
                .Select(item => item.Length);

            Assert.True(option.HasValue);
            Assert.Equal(4, option.Value);
        }

        [Fact]
        public void OptionExtensions_SelectCalledWithLambdaOnOptionWithoutValue_IsNoneShouldBeTrue()
        {
            var option = Option.None<string>()
                .Select(item => item.Length);

            Assert.True(option.IsNone);
        }

        #endregion
    }

}
