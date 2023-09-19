using PayrollManagement.BuildingBlocks.Domain.Primitives;

namespace PayrollManagement.Users.Domain.Users.Events
{
    public class UserCreateDomainEvent : DomainEventBase
    {
        public new UserId UserId { get;  }

        public UserCreateDomainEvent(UserId userId)
        {
            UserId = userId;
        }
    }
}
