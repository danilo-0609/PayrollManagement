using Microsoft.EntityFrameworkCore;
using PayrollManagement.Infraestructure.Employees.Persistence;

namespace PayrollManagement.API.Extensions
{
    public static class MigrationExtensions
    {
        public async static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}
    