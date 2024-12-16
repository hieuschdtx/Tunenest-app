using System.Net;
using tunenest.Application.Features.Administrators.Commands.Login.Events.RefreshTokenCreated;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Commons.Interfaces.Commands;
using tunenest.Domain.Entities;
using tunenest.Domain.Entities.Administrators;
using tunenest.Domain.Exceptions;
using tunenest.Domain.Helpers;
using tunenest.Domain.Interfaces;
using tunenest.Infrastructure.Configurations;
using tunenest.Infrastructure.Services.Authentications;

namespace tunenest.Application.Features.Administrators.Commands.Login
{
    public class LoginAdministratorCommandHandler : ICommandHandler<LoginAdministratorCommand, CreateSuccessResult<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthenticationService _authenticationService;

        public LoginAdministratorCommandHandler(IUnitOfWork unitOfWork, IAuthenticationService authenticationService,
            IAdminRepository adminRepository)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
            _adminRepository = adminRepository;
        }

        public async Task<CreateSuccessResult<string>> Handle(LoginAdministratorCommand request, CancellationToken cancellationToken)
        {
            var administrator = await _adminRepository.GetByEmailAsync(request.email);

            if (administrator is null)
                throw new BusinessRuleException("email", "Tài khoản không đúng!", HttpStatusCode.Unauthorized);

            if (!administrator.CheckPassword(request.password))
            {
                administrator.SetSetLoginFailedCount();
                throw new BusinessRuleException("password", "Mật khẩu không đúng!", HttpStatusCode.Unauthorized);
            }

            var role = await _unitOfWork.Repository<Role, Guid>().GetByIdAsync(administrator.role_id);

            var ClaimAdmin = new ClaimInformation(
                administrator.id.ToString(),
                administrator.user_name,
                administrator.email,
                administrator.phone_number,
                administrator.avatar_url,
                administrator.permission.RootElement.GetRawText(),
                role.name);

            var accessToken = _authenticationService.GenerateAccessTokenAsync(ClaimAdmin);

            var refreshToken = _authenticationService.GenerateRefreshToken();

            var refreshTokenEvent = new RefreshTokenCreatedEvent(new AdministratorRefreshToken(refreshToken, administrator.id));

            administrator.AddDomainEvent(refreshTokenEvent);

            return new CreateSuccessResult<string>("Đăng nhập thành công", accessToken);
        }
    }
}
