using ErrorOr;
using PayrollManagement.Domain.Users;

namespace PayrollManagement.Application.Users.Login
{
    public record LoginUserCommand(string Email, string Password)
                : IRequest<ErrorOr<string>>; 
}
