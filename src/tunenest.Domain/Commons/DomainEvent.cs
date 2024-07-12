using MediatR;

namespace tunenest.Domain.Commons
{
    public abstract class DomainEvent : INotification
    {
        public DateTime date_occurred { get; protected set; } = DateTime.Now;
    }
}