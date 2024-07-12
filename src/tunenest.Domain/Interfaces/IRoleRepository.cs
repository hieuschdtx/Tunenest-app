using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Entities;

namespace tunenest.Domain.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Role, Guid>
    {
    }
}
