using EmployeeSystem.Application.Interfaces.Persistence;
using EmployeeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Infrastructure.Persistence.DataAccess;

public class TaskRepository : BaseRepository<EmployeeTask>, ITaskRepository
{
    public TaskRepository(SystemContext context) : base(context)
    {
    }

    public async Task<IEnumerable<EmployeeTask>> GetEmployeeTasksAsync(string userName)
    {
        return await _set.Include(t => t.Employee)
                         .Where(t => t.Employee.UserName == userName)
                         .ToListAsync();
    }
}