using PayrollManagement.BuildingBlocks.Domain.Abstractions;
using PayrollManagement.BuildingBlocks.Domain.Primitives;
using PayrollManagement.Users.Domain.Users.Events;
using PayrollManagement.Users.Domain.ValueObjects;

namespace PayrollManagement.Users.Domain.Users
{
    public sealed class User : Entity, IAggregateRoot
    {
        public UserId UserId { get; private set; }

        public UserName UserName { get; private set; }

        public Email Email { get; private set; }

        public Password Password { get; private set; }

        public Role Role { get; private set; }

        public User(UserId userId,
            UserName userName,
            Email email,
            Password password,
            Role role)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Password = password;
            Role = role;

            AddDomainEvent(new UserCreateDomainEvent(userId));
        }

        private User()
        {

        }
        
        public static User UpdateUser(UserId userId,
            UserName userName, 
            Email email, 
            Password password,
            Role role
            )
        {
            return new User(userId, userName, email, password, role);
        } 
    }
}
