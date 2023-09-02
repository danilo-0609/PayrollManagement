using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PayrollManagement.Application.Common.Behavior;

namespace PayrollManagement.Application
{
    public static class ApplicationDependencyInjection 
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            });

            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidatorBehavior<,>)
                );

            services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly);
            
            return services;

        }
    }
}