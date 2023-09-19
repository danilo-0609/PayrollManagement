using ErrorOr;
using MediatR;
using PayrollManagement.Users.Application.Users.Common;

namespace PayrollManagement.Users.Application.Users.Login
{
    public sealed record LoginCommand(string Email, string Password) : IRequest<ErrorOr<TokenResponseDto>>; 
}
