using ErrorOr;

namespace PayrollManagement.Application.Users.Login
{
    public record LoginCommand(string Email, string UserName, string Password)
                : IRequest<ErrorOr<string>; 
}
