namespace tunenest.Application.Commons
{
    public static class ErrorValidationMessage
    {
        public const string ErrorLengthProperty = "Độ dài không quá 225 kí tự";

        public static string ErrorEmptyProperty(this string propertyName)
        {
            return $"{propertyName} không được để trống";
        }

        public static string ErrorInvalidProperty(this string propertyName)
        {
            return $"{propertyName} không hợp lệ";
        }
    }
}
