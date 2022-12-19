using System.Text;
using EmployeeSystem.Application.Interfaces.Persistence;
using EmployeeSystem.Application.Interfaces.Services;
using EmployeeSystem.Application.Options;
using EmployeeSystem.Infrastructure.Identity;
using EmployeeSystem.Infrastructure.Identity.Entities;
using EmployeeSystem.Infrastructure.Mapping;
using EmployeeSystem.Infrastructure.Persistence;
using EmployeeSystem.Infrastructure.Persistence.DataAccess;
using EmployeeSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeSystem.API.Extensions;

public static class ServicesExtensions
{
    public static void AddDbConnections(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(DbConnectionsOptions.SectionName)
                                   .Get<DbConnectionsOptions>();

        services.AddDbContext<SystemContext>(opt => opt.UseSqlServer(options.SystemDb));
        services.AddDbContext<IdentityContext>(opt => opt.UseSqlServer(options.IdentityDb));
    }

    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
    }

    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(JwtOptions.SectionName)
                                  .Get<JwtOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = options.Issuer,

                        ValidateAudience = true,
                        ValidAudience = options.Audience,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)),
                    };
                });
    }

    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<UserIdentity>(opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequiredLength = 5;
                })
                .AddRoles<UserRole>()
                .AddRoleManager<RoleManager<UserRole>>()
                .AddSignInManager<SignInManager<UserIdentity>>()
                .AddRoleValidator<RoleValidator<UserRole>>()
                .AddEntityFrameworkStores<IdentityContext>();
    }

    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperProfile).Assembly);
    }
}