namespace tunenest.Domain.Commons.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAndClearEvents(IEnumerable<IEntity> entitiesWithEvents);
    }
}
