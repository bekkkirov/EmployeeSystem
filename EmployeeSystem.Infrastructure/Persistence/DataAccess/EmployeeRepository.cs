using EmployeeSystem.Application.Interfaces.Persistence;
using EmployeeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Infrastructure.Persistence.DataAccess;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(SystemContext context) : base(context)
    {
    }

    public async Task<Employee?> GetByUserNameAsync(string userName)
    {
        return await _set.FirstOrDefaultAsync(e => e.UserName == userName);
    }

    public async Task<Employee?> GetWithAssignedTasksAsync(string userName)
    {
        return await _set.Include(u => u.AssignedTasks)
                         .FirstOrDefaultAsync(u => u.UserName == userName);
    }
}