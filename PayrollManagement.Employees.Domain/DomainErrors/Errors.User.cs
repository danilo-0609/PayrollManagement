using ErrorOr;

namespace PayrollManagement.Domain.DomainErrors
{
    public static class Errors
    {
        public static class User
        {
            public static Error UserEmailNotFound =>
                    Error.NotFound("User.Email", "The email provided was not found.");

            public static Error UserNotFound =>
                Error.NotFound("User.NotFound", "The user was not found.");

            public static Error UserRegisteredAlready =>
                Error.Validation("User.Register", "The user information provided for registration " +
                    "is registered already");

            public static Error UsernameWithBadFormat =>
                Error.Validation("User.UserName", "The username format received had not a valid format.");

            public static Error PasswordWithBadFormat =>
                Error.Validation("User.Password", "The password format received had not a valid format.");
        }
    }
}
