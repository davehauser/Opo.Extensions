
using System;
using System.Linq;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static string RemoveEmptyLines(this string s)
		{
			if (s.IsNullOrWhiteSpace())
			{
				return null;
			}

			var lines = s.ToLines().Where(x => x.IsNotNullOrWhiteSpace());
			return String.Join(Environment.NewLine, lines);
		}
	}
}
