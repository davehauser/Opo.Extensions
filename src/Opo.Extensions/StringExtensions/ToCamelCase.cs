namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static string ToCamelCase(this string s)
		{
			if (s == null)
			{
				return null;
			}

			return s.ToPascalCase().FirstLetterToLowerCase();
		}
	}
}