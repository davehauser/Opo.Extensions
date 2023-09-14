using System.Collections.Generic;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static string ReplaceIllegalFilenameCharacters(this string s)
		{
			var replacementMap = new Dictionary<string, string>
			{
				{"\\", "-" }, {"/", "-"}, {"*", ""}, {"?", "" },
				{"\"", "'"}, {"<", ""}, {">", ""}, {"|", "-"}
			};
			return s.ReplaceCharacters(replacementMap);
		}
	}
}
