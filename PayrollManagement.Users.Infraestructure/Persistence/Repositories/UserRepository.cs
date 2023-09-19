using Microsoft.EntityFrameworkCore;
using PayrollManagement.Users.Domain.Users;
using PayrollManagement.Users.Domain.ValueObjects;

namespace PayrollManagement.Users.Infraestructure.Persistence.Repositories
{
    public class UserRepository
        : IUserRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(
                nameof(ApplicationDbContext));
        }

        public async Task CreateUserAsync(User user) => await _dbContext.Users.AddAsync(user);


        public void DeleteUser(User user) => _dbContext.Users.Remove(user);


        public async Task<List<User>> GetAllUsers() => await _dbContext.Users.ToListAsync();


        public async Task<User?> GetByIdAsync(UserId userId, 
            CancellationToken cancellationToken)
        {
            return await _dbContext.Users.
                        SingleOrDefaultAsync(d => d.UserId == userId, cancellationToken);
        }

        public async Task<User?> GetUserByEmailAsync(Email email, 
            CancellationToken cancellationToken)
        {
            return await _dbContext.Users.
                        SingleOrDefaultAsync(d => d.Email == email, cancellationToken);
        }

        public void UpdateUser(User user) => _dbContext.Users.Update(user);


        public async Task<bool> UserEmailExistsAsync(string email) => await _dbContext.Users.AnyAsync(d => d.Email == Email.Create(email));
        
    }
}
