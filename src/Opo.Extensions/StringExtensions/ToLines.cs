using System;
using System.Collections.Generic;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static IEnumerable<string> ToLines(this string s, bool removeEmptyLines = true)
		{
			if (s.IsNullOrEmpty())
			{
				return new string[] { };
			}

			var options = removeEmptyLines ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
			return s.Split(new[] { "\r\n", "\r", "\n" }, options);
		}
	}
}
