using System;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static bool IsNullOrWhiteSpace(this string s)
		{
			return String.IsNullOrWhiteSpace(s);
		}

		public static bool IsNotNullOrWhiteSpace(this string s)
		{
			return !String.IsNullOrWhiteSpace(s);
		}
	}
}
