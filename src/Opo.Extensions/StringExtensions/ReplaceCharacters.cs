using System.Collections.Generic;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static string ReplaceCharacters(this string s, IDictionary<string, string> replacementMap)
		{
			if (s.IsNullOrEmpty())
			{
				return s;
			}

			var output = s;
			foreach (var item in replacementMap)
			{
				output = output.Replace(item.Key, item.Value);
			}
			return output;
		}
	}
}
