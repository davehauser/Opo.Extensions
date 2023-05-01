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

        [Fact]
        public void ToIso8601String_WithNullInput_ReturnsEmptyString()
        {
            //Given
            var input = default(DateTime?);

            //When
            var actual = input.ToIso8601String();

            //Then
            actual.ShouldBeEmpty();
        }

        [Fact]
        public void ToIso8601String_ReturnsCorrectFormattedString()
        {
            //Given
            var input = new DateTime(2000, 1, 2, 3, 4, 5);

            //When
            var actual = input.ToIso8601String();

            //Then
            actual.ShouldBe("2000-01-02T03:04:05");
        }
	}
}
