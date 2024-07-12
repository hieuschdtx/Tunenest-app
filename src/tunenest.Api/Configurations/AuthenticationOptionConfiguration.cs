using Microsoft.Extensions.Options;
using tunenest.Infrastructure.Options;

namespace tunenest.Api.Configurations
{
    public class AuthenticationOptionConfiguration : IConfigureOptions<AuthenticationOption>
    {
        private const string sectionName = "Jwt";
        private readonly IConfiguration _configuration;

        public AuthenticationOptionConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(AuthenticationOption options)
        {
            _configuration.GetSection(sectionName).Bind(options);
        }
    }
}
