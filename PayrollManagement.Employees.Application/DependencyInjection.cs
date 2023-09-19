using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PayrollManagement.Application.Common.Behavior;

namespace PayrollManagement.Application
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddEmployeeApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining
                                                <EmployeeApplicationAssemblyReference>();
            });

            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidatorBehavior<,>)
                );

            services.AddValidatorsFromAssembly(EmployeeApplicationAssemblyReference.Assembly);
            
            return services;

        }
    }
}