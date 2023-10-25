using System.Collections.Generic;
using System.Linq;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static IList<string> SplitString(this string s, char separator = ',', bool trimEntries = true, bool removeEmptyEntries = true)
		{
			return s.SplitString(new[] { separator }, trimEntries, removeEmptyEntries);
		}

		public static IList<string> SplitString(this string s, IEnumerable<char> separators, bool trimEntries = true, bool removeEmptyEntries = true)
		{
			if (s == null)
			{
				return new List<string>();
			}

			if (separators == null)
			{
				separators = new[] { ',' };
			}
			IEnumerable<string> results = s.Split(separators.ToArray());

			if (trimEntries)
			{
				results = results.Select(x => x.Trim());
			}

			return (removeEmptyEntries
				? results.Where(x => x.IsNotNullOrEmpty())
				: results
			).ToList();
		}
	}
}
