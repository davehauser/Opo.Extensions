using System;
using System.Collections.Generic;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		[Obsolete]
		public static List<string> ToLinesList(this string s)
		{
			if (s.IsNullOrEmpty())
			{
				return new List<string>();
			}

			return new List<string>(s.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
		}
	}
}
