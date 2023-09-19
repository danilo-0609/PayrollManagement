using PayrollManagement.BuildingBlocks.Domain.Abstractions;
using PayrollManagement.BuildingBlocks.Domain.Primitives;
using PayrollManagement.Users.Domain.UserRegistration.Events;
using PayrollManagement.Users.Domain.UserRegistration.Rules;
using PayrollManagement.Users.Domain.Users;
using PayrollManagement.Users.Domain.ValueObjects;
using System.Runtime.CompilerServices;

namespace PayrollManagement.Users.Domain.UserRegistration
{
    public class UserRegistration : Entity, IAggregateRoot
    {
        public UserRegistrationId Id { get; private set; }

        private string _userName;

        private string _password;

        private string _email;

        private string _firstName;

        private string _lastName;

        private string _name;

        private string _role;

        private DateTime _registerDate;

        private UserRegistrationStatus _status;

        private DateTime _confirmedDate;

        private UserRegistration()
        {

        }

        public static UserRegistration RegisterNewUser(
           string login,
           string password,
           string email,
           string firstName,
           string lastName,
           IUsersCounter usersCounter,
           string confirmLink)
        {
            return new UserRegistration(login, password, email, firstName, lastName, usersCounter, confirmLink);
        }

        private UserRegistration(
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            IUsersCounter usersCounter,
            string confirmLink)
        {
            CheckRule(new UserLoginMustBeUniqueRule(usersCounter, login));

            this.Id = new UserRegistrationId(Guid.NewGuid());
            _userName = login;
            _password = password;
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _name = $"{firstName} {lastName}";
            _registerDate = DateTime.UtcNow;
            _status = UserRegistrationStatus.WaitingForConfirmation;

            AddDomainEvent(new NewUserRegisteredDomainEvent(
                Id,
                _userName,
                _email,
                _firstName,
                _lastName,
                _name,
                _registerDate,
                confirmLink));
        }

        public User CreateUser()
        {
            CheckRule(new UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(_status));

            var userId = new UserId(Guid.NewGuid());

            return new User(userId ,UserName.Create(_userName)!,
                Email.Create(_email)!, Password.Create(_password)!, Role.Create(_role)!);

        }

        public void Confirm()
        {
            CheckRule(new UserRegistrationCannotBeConfirmedMoreThanOnceRule(_status));
            CheckRule(new UserRegistrationCannotBeConfirmedAfterExpirationRule(_status));

            _status = UserRegistrationStatus.Confirmed;
            _confirmedDate = DateTime.UtcNow;

            AddDomainEvent(new UserRegistrationConfirmedDomainEvent(Id));
        }

        public void Expire()
        {
            CheckRule(new UserRegistrationCannotBeExpiredMoreThanOnceRule(_status));

            _status = UserRegistrationStatus.Expired;

            AddDomainEvent(new UserRegistrationExpiredDomainEvent(Id));
        }
    }
}
