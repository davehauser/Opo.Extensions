using System;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static bool IsMultiLine(this string s)
		{
			return s.Contains(Environment.NewLine);
		}
	}
}
