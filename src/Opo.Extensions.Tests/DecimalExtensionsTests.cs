using Shouldly;
using Xunit;

namespace Opo.Extensions
{
    public class DecimalExtensionsTests
    {
        [Fact]
        public void ToStringWithDefault_ReturnsFormattedString()
        {
            // arrange
            decimal? d = 123.456m;
            var format = "0.00";
            var expected = "123.46";

            // act
            var actual = d.ToStringWithDefault("default", format);

            // assert
            actual.ShouldBe(expected);
        }

        [Fact]
        public void ToStringWithDefault_NullDecimal_ReturnsDefaultValue()
        {
            // arrange
            decimal? d = null;
            var defaultValue = "default";
            var expected = defaultValue;

            // act
            var actual = d.ToStringWithDefault(defaultValue, "0");

            // assert
            actual.ShouldBe(expected);
        }
    }
}
