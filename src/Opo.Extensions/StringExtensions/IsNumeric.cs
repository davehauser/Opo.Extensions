using System.Text.RegularExpressions;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static bool IsNumeric(this string s)
		{
			if (s.IsNullOrWhiteSpace())
			{
				return false;
			}

			var regex = new Regex(@"^(\-)?(\d*(\.(\d)+)?)$");
			return regex.IsMatch(s);
		}
	}
}
