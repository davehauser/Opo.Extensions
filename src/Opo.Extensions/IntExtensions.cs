namespace Opo.Extensions
{
    public static class IntExtensions
    {
        public static string ToStringWithDefault(this int? i, string defaultValue = "", int padLeft = 0)
        {
            if(!i.HasValue) {
                return defaultValue;
            }

            var result = $"{i}";
            return result.PadLeft(padLeft, '0');
        }
    }
}