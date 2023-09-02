using PayrollManagement.API.Middlewares;

namespace PayrollManagement.API
{
    public static class ApiDependencyInjection
    {
        public static IServiceCollection AddPresentationApi(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddTransient<GlobalExceptionHandlingMiddleware>();

            return services;
        }
    }
}
