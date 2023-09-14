using System.Net;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static string HtmlEncode(this string s)
		{
			return s.IsNullOrEmpty()
				? s
				: WebUtility.HtmlEncode(s);
		}
	}
}
