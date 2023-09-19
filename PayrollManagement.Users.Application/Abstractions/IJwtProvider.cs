using PayrollManagement.Users.Domain.Users;

namespace PayrollManagement.Users.Application.Abstractions
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
