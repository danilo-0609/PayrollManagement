using PayrollManagement.Users.Domain.ValueObjects;

namespace PayrollManagement.Users.Application.Users.Common
{
    public record UserResponseDto(UserName UserName, Email Email);
}
