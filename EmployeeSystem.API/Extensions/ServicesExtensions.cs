using EmployeeSystem.Application.Options;
using EmployeeSystem.Infrastructure.Persistence;
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
}