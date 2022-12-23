using AutoMapper;
using EmployeeSystem.Infrastructure.Mapping;
using EmployeeSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.UnitTests.Helpers;

public static class UnitTestsHelpers
{
    public static IMapper CreateMapperProfile()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperProfile());
        });

        return config.CreateMapper();
    }

    public static DbContextOptions<SystemContext> GetInMemoryContextOptions()
    {
        var options = new DbContextOptionsBuilder<SystemContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        return options;
    }
}