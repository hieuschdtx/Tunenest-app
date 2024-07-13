namespace tunenest.Domain.Commons.Interfaces
{
    public interface IEntity
    {
        void ClearDomainEvents();

        IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    }
}
