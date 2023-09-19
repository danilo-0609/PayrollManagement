using PayrollManagement.Users.Domain.ValueObjects;

namespace PayrollManagement.Users.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken);
        Task<List<User>> GetAllUsers();
        Task<bool> UserEmailExistsAsync(string email); 
        Task CreateUserAsync(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        Task<User?> GetUserByEmailAsync(Email email, 
            CancellationToken cancellationToken);
    }
}
