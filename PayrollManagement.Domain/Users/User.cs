using PayrollManagement.Domain.Primitives;
using PayrollManagement.Domain.ValueObjects;

namespace PayrollManagement.Domain.Users
{
    public sealed class User : AggregateRoot
    {
        public UserId UserId { get; private set; } 

        public UserName UserName { get; private set; }

        public Email Email { get; private set; }

        public Password Password { get; private set; }

        public string Role { get; private set; }

        public User(UserId userId, UserName userName, Email email, Password password, string role)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Password = password;
            Role = role;
        }

        private User()
        {

        }

        public User(UserName userName, Email email, Password password)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }

        public User(UserName userName, Email email, Password password, string role)
        {
            UserName = userName;
            Email = email;
            Password = password;
            Role = role;
        }

        public static User UpdateUser(UserId userId, 
            UserName userName, 
            Email email, 
            Password password,
            string role)
        {
            return new User(userId, userName, email, password, role);
        }

        public static User UpdateUser(UserName 
            userName, 
            Email email, 
            Password password)
        {
            return new User(userName, email, password); 
        }

        public static User UpdateUser(UserName
            userName,
            Email email,
            Password password, 
            string role)
        {
            return new User(userName, email, password, role);
        }

    }
}
