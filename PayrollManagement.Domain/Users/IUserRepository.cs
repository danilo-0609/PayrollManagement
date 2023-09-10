using ErrorOr;
using PayrollManagement.Domain.ValueObjects;

namespace PayrollManagement.Domain.Users
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(UserId userId);
        Task<bool> UserEmailExistsAsync(string email);
        void DeleteUser(User user);
        void UpdateUser(User user);
        void CreateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(Email email, CancellationToken cancellationToken);
    }
}
