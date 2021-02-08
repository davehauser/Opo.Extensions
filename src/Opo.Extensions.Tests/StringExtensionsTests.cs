using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Shouldly;
using Xunit;

namespace Opo.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void SplitString_ReturnsTrimmedNonEmptyEntriesFromCommaSparatedString_IfNoArgumentsArePassed()
        {
            //Given
            var s = "A, B, ,C ";

            //When
            var actual = s.SplitString();

            //Then
            actual.ShouldBe(new[] { "A", "B", "C" });
        }

        [Fact]
        public void SplitString_RespectsDifferentSeparator()
        {
            //Given
            var s = "A| B| |C ";

            //When
            var actual = s.SplitString('|');

            //Then
            actual.ShouldBe(new[] { "A", "B", "C" });
        }

        [Fact]
        public void SplitString_DontRemoveEmptyEntries_IfSpecified()
        {
            //Given
            var s = "A, B, ,C ";

            //When
            var actual = s.SplitString(removeEmptyEntries: false);

            //Then
            actual.ShouldBe(new[] { "A", "B", "", "C" });
        }

        [Fact]
        public void SplitSring_DontTrimEntries_IfSpecified()
        {
            //Given
            var s = "A, B, ,C ";

            //When
            var actual = s.SplitString(trimEntries: false);

            //Then
            actual.ShouldBe(new[] { "A", " B", " ", "C " });
        }

        [Theory]
        [InlineData("1,2,3,4,56")]
        [InlineData("1 2ab3ab4-ei%56")]
        [InlineData("ölkj1j2 3.4ö56er")]
        public void ParseIntegers_ReturnsAllIntegers(string input)
        {
            //Given
            var expected = new int[] { 1, 2, 3, 4, 56 };

            //When
            var actual = input.ParseIntegers();

            //Then
            actual.ShouldBe(expected);
        }

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void ToLines_ReturnsEmptyArray_WhenStringIsNullOrEmpty(string input)
		{
			//Given
			var expected = new string[] { };

			//When
			var actual = input.ToLines();

			//Then
			actual.ShouldBe(expected);
		}

		[Fact]
		public void ToLines_ReturnsNoEmptyLines_WheneOptionsIsSet()
		{
			//Given
			var input = @"ABC" + Environment.NewLine + Environment.NewLine + "DEF";
			var expected = new string[] { "ABC", "DEF" };
			
			//When
			var actual = input.ToLines();

			//Then
			actual.ShouldBe(expected);
		}
    }
}