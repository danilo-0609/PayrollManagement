namespace PayrollManagement.BuildingBlocks.Domain.Primitives
{
    public abstract class AggregateRoot
    {
        private readonly List<DomainEventBase> _domainEvents = new();

        public ICollection<DomainEventBase> GetDomainEvents() => _domainEvents;

        protected void Raise(DomainEventBase domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
