
namespace Opo.Extensions
{
	public static partial class StringExtensions
	{
		public static string IncrementLetter(this string letter)
		{
			return letter.IsNullOrEmpty()
				? "a"
				: ((char)(letter[0] + 1)).ToString();
		}
	}
}
