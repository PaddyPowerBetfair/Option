using Xunit;

namespace PaddyPowerBetfair.Common.Maybe.Tests.Unit
{
   
    public class OptionTests
    {

        #region Option.ValueOr

        [Fact]
        public void Option_WhenOptionContainsValue_ValueOrShouldProvideContainedValue()
        {
            var option = Option.Some("test");
            var check = option.ValueOr(string.Empty);

            Assert.Equal("test", check);

        }

        [Fact]
        public void Option_WhenOptionDoesNotContainValue_ValueOrShouldProvideParameterValue()
        {
            var option = Option.None<string>();
            var check = option.ValueOr("test");

            Assert.Equal("test", check);
        }

        #endregion

        #region Equals

        [Fact]
        public void Option_WhenTwoOptionsContainingTheSameReferenceValueAreCompared_EqualsShouldReturnTrue()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.Some("test");

            Assert.True(firstOption.Equals(secondOption));
        }

        [Fact]
        public void Option_WhenOptionContainingReferenceValueComparedWithNoneOption_EqualsShouldReturnFalse()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.None<string>();

            Assert.False(firstOption.Equals(secondOption));
        }

        [Fact]
        public void Option_WhenOptionsContainingDifferentReferenceValuesAreCompared_EqualsShouldReturnFalse()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.Some("test2");

            Assert.False(firstOption.Equals(secondOption));
        }

        [Fact]
        public void Option_WhenOptionsContainingTheSameValueAreCompared_EqualsShouldReturnTrue()
        {
            var firstOption = Option.Some(10);
            var secondOption = Option.Some(10);

            Assert.True(firstOption.Equals(secondOption));
        }

        [Fact]
        public void Option_WhenOptionsContainingDifferentValuesAreCompared_EqualsShouldReturnFalse()
        {
            var firstOption = Option.Some(10);
            var secondOption = Option.Some(11);

            Assert.False(firstOption.Equals(secondOption));
        }

        #endregion

        #region Options == operator
        [Fact]
        public void Option_WhenTwoOptionsContainingTheSameReferenceValueAreComparedWithEqualityOperator_TheyShouldBeEquals()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.Some("test");

            Assert.True(firstOption == secondOption);
        }

        [Fact]
        public void Option_WhenOptionContainingReferenceValueComparedWithNoneOptionWithEqualityOperator_TheyShouldNotBeEquals()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.None<string>();

            Assert.True(firstOption != secondOption);
        }

        [Fact]
        public void Option_WhenOptionsContainingDifferentReferenceValuesAreComparedWithEqualityOperator_TheyShouldNotBeEquals()
        {
            var firstOption = Option.Some("test");
            var secondOption = Option.Some("test2");

            Assert.True(firstOption != secondOption);
        }

        [Fact]
        public void Option_WhenOptionsContainingTheSameValueAreComparedWithEqualityOperator_TheyShouldBeEquals()
        {
            var firstOption = Option.Some(10);
            var secondOption = Option.Some(10);

            Assert.True(firstOption == secondOption);
        }

        [Fact]
        public void Option_WhenOptionsContainingDifferentValuesAreComparedWithEqualityOperator_TheyShouldNotBeEquals()
        {
            var firstOption = Option.Some(10);
            var secondOption = Option.Some(11);

            Assert.True(firstOption != secondOption);
        }

        #endregion

        #region Option.GetHashCode


        [Fact]
        public void Option_WhenHashCodeOfOptionContainingIntIsCalculated_HasCodeShouldBeInt()
        {
            var option = Option.Some(1).GetHashCode();

            Assert.IsType<int>(option);
        }

        [Fact]
        public void Option_WhenHashCodeOfOptionContainingStringIsCalculated_HasCodeShouldBeInt()
        {
            var option = Option.Some("test").GetHashCode();

            Assert.IsType<int>(option);
        }

        #endregion

        #region Option.ToString

        [Fact]
        public void Option_WhenToStringCalledOnOptionContainingString_ResultShouldCorrespondToGenericTypeDefintion()
        {
            var option = Option.Some("test").ToString();

            Assert.Equal("Some<String>(test)", option);
        }

        [Fact]
        public void Option_WhenToStringCalledOnOptionContainingInt_ResultShouldCorrespondToGenericTypeDefintion()
        {
            var option = Option.Some(10).ToString();

            Assert.Equal("Some<Int32>(10)", option);
        }

        #endregion

    }
}
