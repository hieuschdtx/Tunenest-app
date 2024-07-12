namespace tunenest.Domain.Extensions
{
    public static class GuidExtension
    {
        public static bool IsGuid(this string value) => Guid.TryParse(value, out _);

        public static Guid ToGuid(this string value) => Guid.TryParse(value, out var guid) ? guid : Guid.Empty;

        public static Guid GetNewGuid() => Guid.NewGuid();

        public static string GetNewGuidString() => Guid.NewGuid().ToString();
    }
}
