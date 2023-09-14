
namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static string FirstLetterToLowerCase(this string s)
		{
			if (s?.Length <= 1)
			{
				return s;
			}

			return s.Substring(0, 1).ToLower() + s.Substring(1);
		}
	}
}
