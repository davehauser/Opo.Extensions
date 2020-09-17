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
    }
}