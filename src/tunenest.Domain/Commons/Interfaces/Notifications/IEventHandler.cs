using MediatR;

namespace tunenest.Domain.Commons.Interfaces.Notifications
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : DomainEvent
    {
    }
}
