using PayrollManagement.Domain.Users;

namespace PayrollManagement.Application.Abstractions
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
