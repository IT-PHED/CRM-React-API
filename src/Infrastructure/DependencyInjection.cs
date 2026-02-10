using System.Text;
using Application.Abstractions.Authentication;
using Application.Abstractions.Common.Service;
using Application.Abstractions.Data;
using Application.Abstractions.Factory;
using Application.Abstractions.Factory.Area;
using Application.Abstractions.Factory.Complaint;
using Application.Abstractions.Factory.ComplaintResolution;
using Application.Abstractions.Factory.Configuration;
using Application.Abstractions.Factory.Consumer;
using Application.Abstractions.Factory.Dashboard;
using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Factory.EscalationMatrixResolution;
using Application.Abstractions.Factory.SLA;
using Application.Abstractions.Repository;
using Infrastructure.Authentication;
using Infrastructure.Authorization;
using Infrastructure.Database;
using Infrastructure.DomainEvents;
using Infrastructure.Factory;
using Infrastructure.Factory.Area;
using Infrastructure.Factory.Complaint;
using Infrastructure.Factory.ComplaintResolution;
using Infrastructure.Factory.Configuration;
using Infrastructure.Factory.Consumer;
using Infrastructure.Factory.Dashboard;
using Infrastructure.Factory.EmployeeManagement;
using Infrastructure.Factory.EscalationMatrixResolution;
using Infrastructure.Factory.Sla;
using Infrastructure.Repository;
using Infrastructure.Time;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SharedKernel;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddServices()
            .AddDatabase(configuration)
            .AddHealthChecks(configuration)
            .AddAuthenticationInternal(configuration)
            .AddAuthorizationInternal();

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();

        ///Factory Services
        services.AddTransient<IOtpHandler, OtpHandler>();
        services.AddTransient<IRazorViewToString, RazorViewToStringService>();

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAppService, AppService>();
        services.AddScoped<IConsumerService, ConsumerService>();
        services.AddScoped<IComplaintService, ComplaintService>();
        services.AddScoped<IConfigurationService, ConfigurationService>();
        services.AddScoped<IAreaService, AreaService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IComplaintResolutionService, ComplaintResolutionService>();
        services.AddScoped<ISmsService, SmsService>();
        services.AddScoped<IEscalationMatrixResolutionService, EscalationMatrixResolutionService>();
        services.AddScoped<IEmployeeManagementService, EmployeeManagementService>();
        services.AddScoped<ISlaService, SlaService>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>(options => options.UseOracle(connectionString).UseSnakeCaseNamingConvention());

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks().AddOracle(configuration.GetConnectionString("Database")!);
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();
        services.AddSingleton<ITripartite, Tripartite>();

        return services;
    }

    private static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddScoped<PermissionProvider>();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }
}
