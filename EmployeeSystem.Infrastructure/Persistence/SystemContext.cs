using EmployeeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Infrastructure.Persistence;

public class SystemContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } = default!;

    public DbSet<EmployeeTask> Tasks { get; set; } = default!;

    public SystemContext(DbContextOptions<SystemContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemContext).Assembly);
    }
}