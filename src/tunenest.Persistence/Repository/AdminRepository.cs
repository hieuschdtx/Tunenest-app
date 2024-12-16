using Microsoft.EntityFrameworkCore;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Entities.Administrators;
using tunenest.Domain.Interfaces;

namespace tunenest.Persistence.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IGenericRepository<Administrator, Guid> _repository;

        public AdminRepository(IGenericRepository<Administrator, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<Administrator> GetByEmailAsync(string email)
        {
            return await _repository.Entities.FirstOrDefaultAsync(x => x.email == email);
        }
    }
}
