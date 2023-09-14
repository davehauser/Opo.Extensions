
namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static string WithDefault(this string s, string defaultValue, bool replaceEmptyValues = true)
		{
			if (s == null || (s.IsNullOrEmpty() && replaceEmptyValues))
			{
				return defaultValue;
			}

			return s;
		}
	}
}
