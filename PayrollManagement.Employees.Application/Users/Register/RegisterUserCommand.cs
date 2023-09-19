namespace PayrollManagement.Application.Users.Register
{
    public record RegisterUserCommand(string UserName, string Email, string Password, string Role) : IRequest<ErrorOr<string>>;
}
