using MediatR;

namespace PayrollManagement.BuildingBlocks.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }

        DateTime OcurredOn { get; }
    }
}
