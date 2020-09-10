using System;

namespace Goodboy.Practices.Extensions
{
    /// <summary>
    /// Helper to handle conversions between dates and longs
    /// </summary>
    public static class DateResolver
    {
        public static DateTime FromUnixTimeMilliseconds(this long dateLong)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(dateLong).DateTime;
        }

        public static DateTime? FromUnixTimeMilliseconds(this long? dateLong)
        {
            return dateLong != null ? DateTimeOffset.FromUnixTimeMilliseconds((long)dateLong).DateTime : (DateTime?)null;
        }

        public static long ToUnixTimeMilliseconds(this DateTime date)
        {
            return ((DateTimeOffset)DateTime.SpecifyKind(date, DateTimeKind.Utc)).ToUnixTimeMilliseconds();
        }

        public static long? ToUnixTimeMilliseconds(this DateTime? date)
        {
            return date != null ? ((DateTimeOffset)DateTime.SpecifyKind((DateTime)date, DateTimeKind.Utc)).ToUnixTimeMilliseconds() : (long?)null;
        }

    }
}
