
namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static string FirstLetterToUpperCase(this string s, bool lowerOtherCharacters = true)
		{
			if (s?.Length <= 1)
			{
				return s;
			}

			return s.Substring(0, 1).ToUpper() + (lowerOtherCharacters ? s.Substring(1).ToLower() : s.Substring(1));
		}
	}
}
