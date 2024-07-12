using System.Data;

namespace tunenest.Domain.Commons.Interfaces
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
