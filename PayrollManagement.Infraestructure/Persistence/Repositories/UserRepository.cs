using Microsoft.EntityFrameworkCore;
using PayrollManagement.Domain.Users;
using PayrollManagement.Domain.ValueObjects;

namespace PayrollManagement.Infraestructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); 
        }

        public async void CreateUserAsync(User user) => await _dbContext.Users.AddAsync(user);

        public void DeleteUser(User user) => _dbContext.Users.Remove(user);

        public async Task<List<User>> GetAllUsersAsync() => await _dbContext.Users.ToListAsync();

        public async Task<User?> GetUserByEmailAsync(Email email, CancellationToken cancellationToken) => await _dbContext.Users.SingleOrDefaultAsync(d => d.Email == email);

        public async Task<User?> GetUserByIdAsync(UserId userId) => await _dbContext.Users.SingleOrDefaultAsync(d => d.UserId == userId);

        public void UpdateUser(User user) => _dbContext.Users.Update(user);

        public async Task<bool> UserEmailExistsAsync(string email) => !await _dbContext.Users.AnyAsync(d => d.Email == Email.Create(email));
        
    }
}
