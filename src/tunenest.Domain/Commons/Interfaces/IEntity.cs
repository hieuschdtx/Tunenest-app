namespace tunenest.Domain.Commons.Interfaces
{
    public interface IEntity : IAuditableEntity
    {
        void ClearDomainEvents();

        IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    }
}
