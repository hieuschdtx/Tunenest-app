namespace tunenest.Application.Services.AdminServices
{
    public interface IAdminService
    {
        Task<bool> ExistEmailAsync(string email);
        Task<bool> ExistPhoneNumberAsync(string phoneNumber);
    }
}
