using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PayrollManagement.API;
using PayrollManagement.API.Extensions;
using PayrollManagement.API.Middlewares;
using PayrollManagement.API.OptionsSetup;
using PayrollManagement.Application;
using PayrollManagement.Infraestructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddPresentationApi()
                .AddInfraestructure(builder.Configuration)
                .AddEmployeeApplication();

builder.Services.AddHttpsRedirection(opt => opt.HttpsPort = 443);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001);
    options.ListenAnyIP(7001, configure => configure.UseHttps());
});

builder.Services.AddAuthentication(auth => 
       auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build()
        );
});


builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

var app = builder.Build();


if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseCors(builder => builder
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());

app.UseRouting(); 
app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
