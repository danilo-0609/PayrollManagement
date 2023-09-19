using MediatR;
using PayrollManagement.BuildingBlocks.Domain.Abstractions;

namespace PayrollManagement.BuildingBlocks.Domain.Primitives
{
    public class DomainEventBase : IDomainEvent
    {
        public Guid Id { get; }

        public DateTime OcurredOn { get; }

        public DomainEventBase()
        {
            Id = Guid.NewGuid();
            OcurredOn = DateTime.UtcNow;
        }
    }
}
