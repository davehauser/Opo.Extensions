using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static string ToPascalCase(this string s)
		{
			if (s == null)
			{
				return null;
			}

			s = Regex.Replace(s, @"\s{2,}|[:,-\.\x5F]\s*", " ").ReplaceIllegalFilenameCharacters().ToUsAscii().Replace("&", "and");
			return String.Join("", s.Split(' ').Select(x => x.FirstLetterToUpperCase()).ToArray());
		}
	}
}
