using System;
using Shouldly;
using Xunit;

namespace Opo.Extensions
{
    public class DateTimeExtensionsTests
    {
        private const string Format = "yyyy-MM-dd hh:mm";
        [Fact]
        public void ToStringWithDefault_ReturnsFormattedString()
        {
            // arrange
            DateTime? dateTime = new DateTime(2000, 1, 1, 0, 0, 0);
            var format = "yyyy-MM-dd HH:mm";
            var expected = "2000-01-01 00:00";

            // act
            var actual = dateTime.ToStringWithDefault("default", format);

            // assert
            actual.ShouldBe(expected);
        }

        [Fact]
        public void ToStringWithDefault_NullDateTime_ReturnsDefaultValue()
        {
            // arrange
            DateTime? dateTime = null;
            var defaultValue = "default";
            var expected = defaultValue;

            // act
            var actual = dateTime.ToStringWithDefault(defaultValue, "yyyy");

            // assert
            actual.ShouldBe(expected);
        }
    
		[Fact]
		public void ToUtc_ReturnsNull_WhenInputValueIsNull()
		{
			//Given
			var input = default(DateTime?);

			//When
			var actual = input.ToUtc();

			//Then
			actual.ShouldBe(null);
		}
	
	}
}
