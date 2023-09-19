using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayrollManagement.Users.Application.Data;
using PayrollManagement.Users.Domain.Primitives;
using PayrollManagement.Users.Domain.Users;
using PayrollManagement.Users.Infraestructure.Persistence.Repositories;

namespace PayrollManagement.Users.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);

            return services;
        }

        private static IServiceCollection AddPersistence(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer")!);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IApplicationDbContext>(sp => 
                    sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<ApplicationDbContext>()
            );

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
