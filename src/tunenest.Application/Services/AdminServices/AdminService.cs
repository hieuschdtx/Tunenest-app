using Dapper;
using tunenest.Domain.Commons.Interfaces;

namespace tunenest.Application.Services.AdminServices
{
    public class AdminService : IAdminService
    {
        private readonly ISqlConnectionFactory _factory;

        public AdminService(ISqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<bool> ExistEmailAsync(string email)
        {
            const string query = "select exists(select 1 from administrators where email = @email)";
            using var conn = _factory.GetOpenConnection();
            return await conn.ExecuteScalarAsync<bool>(query, new { email });
        }

        public async Task<bool> ExistPhoneNumberAsync(string phoneNumber)
        {
            const string query = "select exists(select 1 from administrators where phone_number = @phoneNumber)";
            using var conn = _factory.GetOpenConnection();
            return await conn.ExecuteScalarAsync<bool>(query, new { phoneNumber });
        }
    }
}
