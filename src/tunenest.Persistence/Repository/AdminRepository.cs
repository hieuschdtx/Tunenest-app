using tunenest.Domain.Entities.Administrators;
using tunenest.Domain.Interfaces;
using tunenest.Persistence.Data;

namespace tunenest.Persistence.Repository
{
    public class AdminRepository : GenericRepository<Administrator, Guid>, IAdminRepository
    {
        public AdminRepository(TunenestDbContext context) : base(context)
        {
        }
    }
}
