using tunenest.Domain.Entities.Administrators;

namespace tunenest.Domain.Interfaces
{
    public interface IAdminRepository
    {
        Task<Administrator> GetByEmailAsync(string email);
    }
}
