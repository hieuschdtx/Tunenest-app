using Microsoft.EntityFrameworkCore;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Entities.Administrators;
using tunenest.Domain.Interfaces;

namespace tunenest.Persistence.Repository
{
    public class AdminRefreshTokenRepository : IAdminRefreshTokenRepository
    {
        private readonly IGenericRepository<AdministratorRefreshToken, long> _repository;

        public AdminRefreshTokenRepository(IGenericRepository<AdministratorRefreshToken, long> repository)
        {
            _repository = repository;
        }

        public async Task<AdministratorRefreshToken> GetByAdminId(Guid adminId)
        {
            return await _repository.Entities.FirstOrDefaultAsync(x => x.admin_id == adminId);
        }
    }
}
