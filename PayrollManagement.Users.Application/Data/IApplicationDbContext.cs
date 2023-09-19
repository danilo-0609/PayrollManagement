using Microsoft.EntityFrameworkCore;
using PayrollManagement.Users.Domain.Users;

namespace PayrollManagement.Users.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
