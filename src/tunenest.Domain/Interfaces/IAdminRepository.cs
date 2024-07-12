using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Entities.Administrators;

namespace tunenest.Domain.Interfaces
{
    public interface IAdminRepository : IGenericRepository<Administrator, Guid>
    {
    }
}
