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
		public void ToYesNo_ForBoolean(bool value, BooleanExtensions.Case @case, string expected)
		{
			// act
			var actual = value.ToYesNo(@case);

			// assert
			actual.ShouldBe(expected);
		}

		[Theory]
		[InlineData(true, BooleanExtensions.Case.Capitalize, "default", "Yes")]
		[InlineData(true, BooleanExtensions.Case.Uppercase, "default", "YES")]
		[InlineData(true, BooleanExtensions.Case.Lowercase, "default", "yes")]
		[InlineData(false, BooleanExtensions.Case.Capitalize, "default", "No")]
		[InlineData(false, BooleanExtensions.Case.Uppercase, "default", "NO")]
		[InlineData(false, BooleanExtensions.Case.Lowercase, "default", "no")]
		[InlineData(null, BooleanExtensions.Case.Lowercase, "default", "default")]
		[InlineData(null, BooleanExtensions.Case.Lowercase, null, "")]
		public void ToYesNo_ForNullableBoolean(bool? value, BooleanExtensions.Case @case, string defaultValue, string expected)
		{
			// act
			var actual = value.ToYesNo(@case, defaultValue);

			// assert
			actual.ShouldBe(expected);
		}
	}
}