using System;
using System.Text;

namespace Opo.Extensions
{
  public static partial class StringExtensions
  {
    public static string ToBase64(this string s)
    {
      if (s.IsNullOrEmpty())
      {
        return s;
      }

      return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
    }

    public static string FromBase64(this string s)
    {
      try
      {
        return Encoding.UTF8.GetString(Convert.FromBase64String(s));
      }
      catch
      {
        return null;
      }
    }
  }
}
