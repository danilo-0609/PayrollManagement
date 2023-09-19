using MediatR;

namespace PayrollManagement.Employees.Domain.Primitives
{
    public record DomainEvent(Guid id) : INotification;
    
}
