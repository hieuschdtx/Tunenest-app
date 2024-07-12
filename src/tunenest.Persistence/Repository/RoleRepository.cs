using tunenest.Domain.Entities;
using tunenest.Domain.Interfaces;
using tunenest.Persistence.Data;

namespace tunenest.Persistence.Repository
{
    public class RoleRepository : GenericRepository<Role, Guid>, IRoleRepository
    {
        public RoleRepository(TunenestDbContext context) : base(context)
        {
        }
    }
}
