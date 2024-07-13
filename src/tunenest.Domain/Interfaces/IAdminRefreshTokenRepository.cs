using tunenest.Domain.Entities.Administrators;

namespace tunenest.Domain.Interfaces
{
    public interface IAdminRefreshTokenRepository
    {
        Task<AdministratorRefreshToken> GetByAdminId(Guid adminId);
    }
}
