using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayrollManagement.Application.Abstractions;
using PayrollManagement.Application.Data;
using PayrollManagement.Domain.Employees;
using PayrollManagement.Domain.Primitives;
using PayrollManagement.Domain.Users;
using PayrollManagement.Infraestructure.Authentication;
using PayrollManagement.Infraestructure.Persistence;
using PayrollManagement.Infraestructure.Persistence.Repositories;


namespace PayrollManagement.Infraestructure
{
    public static class InfraestructureDependencyInjection
    {
        public static IServiceCollection AddInfraestructure(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.InstallPersistenceDependencyInjection(configuration);

            return services;
        }

        private static IServiceCollection InstallPersistenceDependencyInjection(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer")!);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}