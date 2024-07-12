using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using tunenest.Infrastructure.Options;

namespace tunenest.Infrastructure.Configurations
{
    public class AuthenticationSetting
    {
        private readonly AuthenticationOption _authOption;
        private SymmetricSecurityKey? _securityKey;

        public AuthenticationSetting(IOptions<AuthenticationOption> authOption)
        {
            _authOption = authOption.Value;
        }

        public int ExpireTime { get; set; } = 2;

        public SymmetricSecurityKey SecurityKey =>
            _securityKey ??= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOption.SecretKey));

        public SigningCredentials GetSigning() => new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

        public EncryptingCredentials GetEncrypting() => new EncryptingCredentials(SecurityKey, JwtConstants.DirectKeyUseAlg,
               SecurityAlgorithms.Aes128CbcHmacSha256);
    }
}
