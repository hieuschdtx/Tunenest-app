using System.Globalization;

namespace tunenest.Domain.Extensions
{
    public static class DateTimeExtension
    {
        public static string GetCurrentTimestampString() => DateTime.Now.ToString("yyyyMMddHHmmss");

        public static string ToShortDateString(this DateTime dateTime, string format = "dd-MM-yyyy") => dateTime.ToString(format);

        public static string ToShortTimeString(this DateTime dateTime) => dateTime.ToString("HH:mm");

        public static string ToShortDateTimeString(this DateTime dateTime, string format = "dd-MM-yyyy HH:mm") => dateTime.ToString(format);

        public static string ToLongDateString(this DateTime dateTime, string format = "dd-MM-yyyy HH:mm:ss") => dateTime.ToString(format);

        public static DateOnly ParseDateOnlyOrDefault(this string date, string format = "dd-MM-yyyy",
        DateOnly defaultValue = default)
        {
            return DateOnly.TryParseExact(date, format, null, DateTimeStyles.None, out var parsedDate)
                ? parsedDate
                : defaultValue;
        }
    }
}
