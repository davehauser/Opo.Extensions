using System.Linq;

namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static int LineCount(this string s, bool countEmptyLines = true)
		{
			if (s.IsNullOrEmpty())
			{
				return 0;
			}

			return countEmptyLines
				? s.ToLines(false).Count()
				: s.ToLines().Count();
		}
	}
}
