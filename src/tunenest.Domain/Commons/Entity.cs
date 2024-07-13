using System.ComponentModel.DataAnnotations.Schema;
using tunenest.Domain.Commons.Interfaces;

namespace tunenest.Domain.Commons
{
    public abstract class Entity<TEntityId> : IEntity
    {
        private readonly List<DomainEvent> _domainEvents = new();
        private TEntityId _id;

        protected Entity()
        {
        }

        public Entity(TEntityId Id)
        {
            this.id = Id;
        }

        public virtual TEntityId id
        {
            get => _id;
            protected set => _id = value;
        }

        [NotMapped]
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);

        public void RemoveDomainEvent(DomainEvent domainEvent) => _domainEvents.Remove(domainEvent);

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
