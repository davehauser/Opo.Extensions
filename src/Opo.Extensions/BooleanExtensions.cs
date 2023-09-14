namespace Opo.Extensions
{
	public static class BooleanExtensions
		{
				public enum Case
				{
						Lowercase,
						Uppercase,
						Capitalize
				}

				public static string ToYesNo(this bool b, Case c = Case.Capitalize)
				{
						switch (c)
						{
								case Case.Lowercase:
										return b ? "yes" : "no";
								case Case.Uppercase:
										return b ? "YES" : "NO";
								default:
										return b ? "Yes" : "No";
						}
				}
		}
}