using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace tunenest.Infrastructure.Configurations
{
    public class AppSetting
    {
        public CookieSettings cookieSettings { get; set; } = new();
        public JwtBearerSetting jwtBearerSetting { get; set; } = new();
    }

    public class CookieSettings
    {
        public string Domain { get; set; } = "localhost";
        public bool HttpOnly { get; set; } = true;
        public string Name { get; set; } = CookieAuthenticationDefaults.AuthenticationScheme;
        public string SameSite { get; set; } = "Lax";
        public bool SecurePolicy { get; set; } = true;
        public int Expires { get; set; } = 2;
    }

    public class JwtBearerSetting
    {
        public readonly string Name = JwtBearerDefaults.AuthenticationScheme;
    }
}
