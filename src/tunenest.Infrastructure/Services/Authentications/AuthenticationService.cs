using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using tunenest.Domain.Consts;
using tunenest.Infrastructure.Configurations;
using tunenest.Infrastructure.Options;

namespace tunenest.Infrastructure.Services.Authentications
{
    public class AuthenticationService : IAuthenticationService
    {
        protected readonly AppSetting _appSetting = new();
        public readonly AuthenticationOption _authOption;
        private readonly AuthenticationSetting _authenticationSetting;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(IOptions<AuthenticationOption> authOption, AuthenticationSetting authenticationSetting,
        IHttpContextAccessor httpContextAccessor)
        {
            _authOption = authOption.Value;
            _authenticationSetting = authenticationSetting;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateAccessTokenAsync(ClaimInformation claim)
        {
            var claims = new Claim[]
        {
            new(ClaimConst.Id, claim.id),
            new(ClaimConst.UserName, claim.user_name),
            new(ClaimConst.Email, claim.email),
            new(ClaimConst.Phone, claim.phone_number),
            new(ClaimConst.Role, claim.role),
            new(ClaimConst.ProfilePicture, claim.profile_picture),
            new(ClaimConst.Permission, claim.permissions)
        };

            var token = new JwtSecurityToken(
                _authOption.Issuer,
                _authOption.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(_authenticationSetting.ExpireTime),
                _authenticationSetting.GetSigning());

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return accessToken;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public void SaveCookiesStorage(string token, string name, int expiresTime)
        {
            if (_httpContextAccessor.HttpContext == null) return;
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(expiresTime)
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append(name, token, cookieOptions);
        }
    }
}
