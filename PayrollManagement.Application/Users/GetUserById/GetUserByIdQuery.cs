using PayrollManagement.Application.Users.Common;

namespace PayrollManagement.Application.Users.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<ErrorOr<UserResponseDto>>;
}
