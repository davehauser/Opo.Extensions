namespace Opo.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToStringWithDefault(this decimal? d, string defaultValue = "", string format = "")
        {
            if(!d.HasValue)
            {
                return defaultValue;
            }

            return d.Value.ToString(format);
        }
    }
}