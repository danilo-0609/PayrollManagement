using PayrollManagement.Domain.ValueObjects;

namespace PayrollManagement.Application.Users.Common
{
    public record UserResponseDto(UserName UserName, Email Email);
}
