using EmployeeSystem.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Infrastructure.Identity;

public class IdentityContext : IdentityDbContext<UserIdentity, UserRole, int>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {

    }
}
