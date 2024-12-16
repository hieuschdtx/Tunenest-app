using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using tunenest.Infrastructure.Configurations;
using tunenest.Infrastructure.Options;

namespace tunenest.Api.Configurations
{
    public class JwtBearerConfiguration : IConfigureOptions<JwtBearerOptions>
    {
        private readonly AuthenticationOption _authOption;
        private readonly AuthenticationSetting _authenticationSetting;

        public JwtBearerConfiguration(AuthenticationSetting authenticationSetting, IOptions<AuthenticationOption> authOption)
        {
            _authOption = authOption.Value;
            _authenticationSetting = authenticationSetting;
        }

        public void Configure(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _authOption.Issuer,
                ValidAudience = _authOption.Audience,
                IssuerSigningKey = _authenticationSetting.SecurityKey
            };
        }
    }
}
