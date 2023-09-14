
using System;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static bool EqualsIgnoreCase(this string s, string value, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
		{
			if (s == null)
			{
				return value == null;
			}
			return s.Equals(value, comparisonType);
		}
	}
}
