using System;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static string Reverse(this string s)
		{
			var letters = s.ToCharArray();
			Array.Reverse(letters);
			return new String(letters);
		}
	}
}
