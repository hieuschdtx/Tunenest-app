using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Commons.Interfaces.Notifications;
using tunenest.Domain.Entities.Administrators;
using tunenest.Domain.Interfaces;

namespace tunenest.Application.Features.Administrators.Commands.Login.Events.RefreshTokenCreated
{
    public class RefreshTokenCreatedEventHandler : IEventHandler<RefreshTokenCreatedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdminRefreshTokenRepository _adminRefreshTokenRepository;

        public RefreshTokenCreatedEventHandler(IUnitOfWork unitOfWork, IAdminRefreshTokenRepository adminRefreshTokenRepository)
        {
            _unitOfWork = unitOfWork;
            _adminRefreshTokenRepository = adminRefreshTokenRepository;
        }

        public async Task Handle(RefreshTokenCreatedEvent notification, CancellationToken cancellationToken)
        {
            if (await _adminRefreshTokenRepository.GetByAdminId(notification.administratorRefresh.admin_id) is not null)
                return;

            await _unitOfWork.Repository<AdministratorRefreshToken, long>().AddAsync(notification.administratorRefresh);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
