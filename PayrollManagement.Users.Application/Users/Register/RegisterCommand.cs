namespace PayrollManagement.Users.Application.Users.Register
{
    public record RegisterCommand(string Email, string UserName, string Password, string Role) 
        : IRequest<ErrorOr<string>>;
}
