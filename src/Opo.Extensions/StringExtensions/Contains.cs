using System;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static bool Contains(this string s, string value, StringComparison comparison)
		{
			if (s == null)
				return false;

			return s.IndexOf(value, comparison) >= 0;
		}
	}
}
