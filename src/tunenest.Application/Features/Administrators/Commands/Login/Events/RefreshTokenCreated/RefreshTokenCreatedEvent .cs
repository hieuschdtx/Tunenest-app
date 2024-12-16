using tunenest.Domain.Commons;
using tunenest.Domain.Entities.Administrators;

namespace tunenest.Application.Features.Administrators.Commands.Login.Events.RefreshTokenCreated
{
    public class RefreshTokenCreatedEvent : DomainEvent
    {
        public RefreshTokenCreatedEvent() { }
        public AdministratorRefreshToken administratorRefresh { get; }

        public RefreshTokenCreatedEvent(AdministratorRefreshToken administratorRefresh)
        {
            this.administratorRefresh = administratorRefresh;
        }
    }
}
