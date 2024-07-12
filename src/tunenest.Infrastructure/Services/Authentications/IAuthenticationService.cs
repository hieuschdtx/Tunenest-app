using tunenest.Infrastructure.Configurations;

namespace tunenest.Infrastructure.Services.Authentications
{
    public interface IAuthenticationService
    {
        string GenerateAccessTokenAsync(ClaimInformation claim);

        void SaveCookiesStorage(string token, string name, int expiresTime);

        string GenerateRefreshToken();

        // Task SignOutAsync();
        // Task<bool> VerifyAccessTokenAsync(string token);
    }
}
