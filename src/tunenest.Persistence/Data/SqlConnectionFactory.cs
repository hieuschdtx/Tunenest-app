using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using tunenest.Domain.Commons.Interfaces;

namespace tunenest.Persistence.Data
{
    public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Dispose();
                }
            }
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new NpgsqlConnection(_configuration.GetConnectionString("Tunnest_DbConnection"));
                _connection.Open();
            }

            return _connection;
        }
    }
}
