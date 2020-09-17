using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Opo.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return String.IsNullOrEmpty(s);
        }

        public static bool IsNotNullOrEmpty(this string s)
        {
            return !String.IsNullOrEmpty(s);
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
            return String.IsNullOrWhiteSpace(s);
        }

        public static bool IsNotNullOrWhiteSpace(this string s)
        {
            return !String.IsNullOrWhiteSpace(s);
        }

        public static string ToPascalCase(this string s)
        {
            if (s == null)
            {
                return null;
            }

            s = Regex.Replace(s, @"\s{2,}|[:,-\.\x5F]\s*", " ").ReplaceIllegalFilenameCharacters().ToUsAscii().Replace("&", "and");
            return String.Join("", s.Split(' ').Select(x => x.ToFirstLetterUpperCase()).ToArray());
        }

        public static bool Contains(this string s, string value, StringComparison comparison)
        {
            if (s == null)
                return false;

            return s.IndexOf(value, comparison) >= 0;
        }

        public static string Default(this string s, string defaultValue, bool replaceEmptyValues = true)
        {
            if (s == null || (s.IsNullOrEmpty() && replaceEmptyValues))
            {
                return defaultValue;
            }

            return s;
        }

        public static string ReplaceIllegalFilenameCharacters(this string s)
        {
            var replacementMap = new Dictionary<string, string>
            {
                {"\\", "-" }, {"/", "-"}, {"*", ""}, {"?", "" },
                {"\"", "'"}, {"<", ""}, {">", ""}, {"|", "-"}
            };
            return s.ReplaceCharacters(replacementMap);
        }

        public static string ReplaceCharacters(this string s, IDictionary<string, string> replacementMap)
        {
            if (s.IsNullOrEmpty())
            {
                return s;
            }

            var output = s;
            foreach (var item in replacementMap)
            {
                output = output.Replace(item.Key, item.Value);
            }
            return output;
        }

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

        public static string ToFirstLetterUpperCase(this string s)
        {
            if (s?.Length <= 1)
            {
                return s;
            }
            else
            {
                return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
            }
        }

        public static bool EqualsIgnoreCase(this string s, string value)
        {
            return s.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsNumeric(this string s)
        {
            if (s.IsNullOrWhiteSpace())
            {
                return false;
            }

            var regex = new Regex(@"^(\-)?(\d*(\.(\d)+)?)$");
            return regex.IsMatch(s);
        }

        public static string Reverse(this string s)
        {
            var letters = s.ToCharArray();
            Array.Reverse(letters);
            return new String(letters);
        }

        public static bool IsMultiLine(this string s)
        {
            return s.Contains(Environment.NewLine);
        }

        public static List<string> ToLinesList(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                return new List<string>();
            }

            return new List<string>(s.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
        }

        public static int LineCount(this string s, bool includeEmptyLines = true)
        {
            if (s.IsNullOrEmpty())
            {
                return 0;
            }

            return includeEmptyLines
                ? s.ToLinesList().Count
                : s.RemoveEmptyLines().ToLinesList().Count();
        }

        public static string RemoveEmptyLines(this string s)
        {
            if (s.IsNullOrWhiteSpace())
            {
                return null;
            }

            var lines = s.ToLinesList().Where(x => x.IsNotNullOrWhiteSpace());
            return String.Join(Environment.NewLine, lines);
        }

        public static string IncrementLetter(this string letter)
        {
            return letter.IsNullOrEmpty()
                ? "a"
                : ((char)(letter[0] + 1)).ToString();
        }

        public static bool IsEmailAddress(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                return false;
            }
            else
            {
                string exp = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";
                return new Regex(exp, RegexOptions.IgnoreCase).IsMatch(s);
            }
        }

        public static string HtmlEncode(this string s)
        {
            return s.IsNullOrEmpty()
                ? s
                : HttpUtility.HtmlEncode(s);
        }

        public static string[] SplitString(this string s, char separator = ',', bool trimEntries = true, bool removeEmptyEntries = true)
        {
            return s.SplitString(new[] { separator }, trimEntries, removeEmptyEntries);
        }
        public static string[] SplitString(this string s, char[] separators, bool trimEntries = true, bool removeEmptyEntries = true)
        {
            if(separators == null)
            {
                separators = new [] { ',' };
            }
            IEnumerable<string> results = s.Split(separators);
            
            if(trimEntries)
            {
                results = results.Select(x => x.Trim());
            }

            return (removeEmptyEntries
                ? results.Where(x => x.IsNotNullOrEmpty())
                : results
            ).ToArray();
        }
    }
}
