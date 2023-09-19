using ErrorOr;
using MediatR;
using PayrollManagement.BuildingBlocks.Domain.Abstractions;
using PayrollManagement.BuildingBlocks.Domain.DomainErrors;

namespace PayrollManagement.BuildingBlocks.Domain.Primitives
{
    public abstract class Entity
    {
        private List<IDomainEvent>? _domainEvents;

        public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();

            _domainEvents.Add(domainEvent);
        }

        protected ErrorOr<Unit> CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                return Errors.User.BusinessRuleBroken(rule);
            }

            return Unit.Value;
        }

    }
}
