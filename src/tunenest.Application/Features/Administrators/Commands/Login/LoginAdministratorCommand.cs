using tunenest.Domain.Commons.Interfaces.Commands;
using tunenest.Domain.Helpers;

namespace tunenest.Application.Features.Administrators.Commands.Login
{
    public class LoginAdministratorCommand : CommandBase<CreateSuccessResult<string>, Guid>
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
