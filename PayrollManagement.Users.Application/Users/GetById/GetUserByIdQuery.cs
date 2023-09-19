using PayrollManagement.Users.Application.Users.Common;

namespace PayrollManagement.Users.Application.Users.GetById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<ErrorOr<UserResponseDto>>;
}
