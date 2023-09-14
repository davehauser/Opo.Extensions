using Shouldly;
using Xunit;

namespace Opo.Extensions
{
	public class BooleanExtensionsTests
	{
		[Theory]
		[InlineData(true, BooleanExtensions.Case.Capitalize, "Yes")]
		[InlineData(true, BooleanExtensions.Case.Uppercase, "YES")]
		[InlineData(true, BooleanExtensions.Case.Lowercase, "yes")]
		[InlineData(false, BooleanExtensions.Case.Capitalize, "No")]
		[InlineData(false, BooleanExtensions.Case.Uppercase, "NO")]
		[InlineData(false, BooleanExtensions.Case.Lowercase, "no")]
		public void ToYesNo(bool value, BooleanExtensions.Case @case, string expected)
		{
			// act
			var actual = value.ToYesNo(@case);

			// assert
			actual.ShouldBe(expected);
		}
	}
}