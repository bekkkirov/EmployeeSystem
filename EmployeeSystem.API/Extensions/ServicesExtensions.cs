using EmployeeSystem.Application.Interfaces.Persistence;
using EmployeeSystem.Application.Options;
using EmployeeSystem.Infrastructure.Persistence;
using EmployeeSystem.Infrastructure.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.API.Extensions;

public static class ServicesExtensions
{
    public static void AddDbConnections(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(DbConnectionsOptions.SectionName)
                                   .Get<DbConnectionsOptions>();

        services.AddDbContext<SystemContext>(opt => opt.UseSqlServer(options.SystemDb));
    }

    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}