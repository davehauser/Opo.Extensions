using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static IEnumerable<int> ParseIntegers(this string s)
		{
			var matches = Regex.Matches(s ?? "", "\\d+");
			foreach (Match match in matches)
			{
				if (int.TryParse(match.Value, out int i))
				{
					yield return i;
				}
			}
		}
	}
}
