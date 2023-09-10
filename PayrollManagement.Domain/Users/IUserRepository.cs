namespace PayrollManagement.Domain.Users
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(UserId userId);
        Task<bool> UserExistsAsync(UserId userId);
        void DeleteUser(User userId);
        void UpdateUser(UserId userId, User user);
        void CreateUser(User user);
    }
}
