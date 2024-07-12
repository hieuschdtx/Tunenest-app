namespace tunenest.Infrastructure.Options
{
    public class AuthenticationOption
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
    }
}
