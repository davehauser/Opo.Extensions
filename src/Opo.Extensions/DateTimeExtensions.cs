using System;
using System.Globalization;
using TimeZoneConverter;

namespace Opo.Extensions
{
	public static class DateTimeExtensions
	{
		public static TimeSpan GetUtcOffset(this DateTime? dateTime, string timeZoneId = null)
		{
			return GetUtcOffset(dateTime ?? DateTime.MinValue, timeZoneId);
		}
		public static TimeSpan GetUtcOffset(this DateTime dateTime, string timeZoneId = null)
		{
			var timeZone = timeZoneId == null
				? TimeZoneInfo.Local
				: GetTimeZoneInfo(timeZoneId);
			return timeZone.GetUtcOffset(dateTime);
		}

		public static DateTime? ToUtc(this DateTime? dateTime, string timeZoneId = null)
		{
			if (!dateTime.HasValue) return null;

			return ToUtc(dateTime ?? DateTime.MinValue, timeZoneId);
		}
		public static DateTime ToUtc(this DateTime dateTime, string timeZoneId = null)
		{
			var timeZone = GetTimeZoneInfo(timeZoneId);
			var utcOffset = GetUtcOffset(dateTime, timeZoneId);
			var utcDateTime = dateTime.Subtract(utcOffset);
			DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
			return utcDateTime;
		}

		public static string ToUtcString(this DateTime dateTime, string timeZoneId = null)
		{
			var timeZone = GetTimeZoneInfo(timeZoneId);
			if (timeZone != null)
			{
				var dateTimeUtc = dateTime.ToUtc(timeZoneId).ToString("yyyy-MM-ddTHH:mm:ss");
				var utcOffset = timeZone.GetUtcOffset(dateTime);
				if (utcOffset >= new TimeSpan(0))
				{
					dateTimeUtc += $"+{utcOffset.Hours:00}:{utcOffset.Minutes:00}";
				}
				else
				{
					dateTimeUtc += $"{utcOffset.Hours:00}:";
					dateTimeUtc += Math.Abs(utcOffset.Minutes).ToString("00");
				}
				return dateTimeUtc;
			}
			return "";
		}
		public static string ToUtcString(this DateTime? dateTime, string timeZoneId = null)
		{
			if (!dateTime.HasValue)
			{
				return "";
			}

			return dateTime.Value.ToUtcString(timeZoneId);
		}

		public static DateTime ToLocal(this DateTime dateTime, string timeZoneId = null)
		{
			var timeZone = timeZoneId == null
				? TimeZoneInfo.Local
				: GetTimeZoneInfo(timeZoneId);

			var offset = (timeZone ?? TimeZoneInfo.Local).GetUtcOffset(dateTime);
			return dateTime.Add(offset);
		}

		public static DateTime Date(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
		}

		public static DateTime FromUnixTimeStamp(long seconds)
		{
			var date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			date = date.AddSeconds(seconds);
			return date;
		}

		public static DateTime? FromString(this string s)
		{
			return DateTime.TryParse(s, out var dateTime)
				? dateTime
				: (DateTime?)null;
		}

		public static bool IsInTheFuture(this DateTime dateTime, string timeZoneId = null)
		{
			return dateTime > DateTime.Now.ToLocal(timeZoneId);
		}
		public static bool IsInTheFuture(this DateTime? dateTime, string timeZoneId = null)
		{
			if (!dateTime.HasValue)
			{
				return false;
			}

			return dateTime.Value.IsInTheFuture(timeZoneId);
		}

		public static bool IsInThePast(this DateTime dateTime, string timeZoneId = null)
		{
			return dateTime <= DateTime.Now.ToLocal(timeZoneId);
		}
		public static bool IsInThePast(this DateTime? dateTime, string timeZoneId = null)
		{
			if (!dateTime.HasValue)
			{
				return true;
			}

			return dateTime.Value.IsInThePast(timeZoneId);
		}

		public static DateTime FirstDayOfCurrentQuarter(this DateTime dateTime)
		{
			var firstMonthOfCurrentQuarter = ((int)Math.Ceiling((dateTime.Month / 3f)) - 1) * 3 + 1;
			return new DateTime(dateTime.Year, firstMonthOfCurrentQuarter, 1);
		}

		public static DateTime LastDayOfCurrentQuarter(this DateTime dateTime)
		{
			return dateTime.FirstDayOfCurrentQuarter().AddMonths(3).AddDays(-1);
		}

		public static string ToStringWithDefault(this DateTime? dateTime, string defaultValue = "", string format = "yyyy-MM-dd HH:mm", IFormatProvider formatProvider = null)
		{
			if (!dateTime.HasValue)
			{
				return defaultValue;
			}

			if (formatProvider == null)
			{
				formatProvider = CultureInfo.CurrentCulture;
			}
			return dateTime.Value.ToString(format, formatProvider);
		}

		public static string ToIso8601String(this DateTime? dateTime, string defaultValue = "")
		{
			if(!dateTime.HasValue)
			{
				return defaultValue;
			}

			return dateTime.Value.ToIso8601String();
		}
		public static string ToIso8601String(this DateTime dateTime)
		{
			return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
		}

		private static TimeZoneInfo GetTimeZoneInfo(string timeZoneId)
		{
			try
			{
				return TZConvert.GetTimeZoneInfo(timeZoneId);
			}
			catch (System.TimeZoneNotFoundException) {
				return TimeZoneInfo.Local;
			}
		}
	}
}