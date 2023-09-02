
using MediatR;

namespace PayrollManagement.Domain.Primitives
{
    public record DomainEvent(Guid id) : INotification;
    
}
