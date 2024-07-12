namespace tunenest.Domain.Extensions
{
    public static class EnumExtension
    {
        public static bool IsValidEnum<T>(this string value) where T : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return Enum.TryParse(value, true, out T enumValue) && Enum.IsDefined(typeof(T), enumValue);
        }

        public static T ParseEnum<T>(this string value) where T : struct, Enum
        {
            Enum.TryParse(value, true, out T enumValue);
            return enumValue;
        }
    }
}
