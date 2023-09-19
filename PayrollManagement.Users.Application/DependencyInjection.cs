using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PayrollManagement.Users.Application.Common.Behavior;

namespace PayrollManagement.Users.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining
                                            <UserApplicationAssemblyReference>();
            });

            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidatorBehavior<,>)
                );

            services.AddValidatorsFromAssembly(UserApplicationAssemblyReference.Assembly);

            return services;
        }
    }
}
