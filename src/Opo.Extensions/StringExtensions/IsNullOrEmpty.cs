using System;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static bool IsNullOrEmpty(this string s)
		{
			return String.IsNullOrEmpty(s);
		}

		public static bool IsNotNullOrEmpty(this string s)
		{
			return !String.IsNullOrEmpty(s);
		}
	}
}
