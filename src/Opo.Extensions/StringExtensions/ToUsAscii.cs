using System;
using System.Text;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static string ToUsAscii(this string s)
		{
			if (s == null)
			{
				return null;
			}

			s = s.Replace("Ä", "Ae")
				.Replace("ä", "ae")
				.Replace("ö", "oe")
				.Replace("ü", "ue")
				.Replace("Ö", "Oe")
				.Replace("Ü", "Ue");
			var encoder = Encoding.GetEncoding("us-ascii", new EncoderReplacementFallback(string.Empty), new DecoderExceptionFallback());
			return new ASCIIEncoding().GetString(encoder.GetBytes(s));
		}
	}
}
