using EmployeeSystem.Application.Interfaces.Persistence;
using EmployeeSystem.Domain.Entities;

namespace EmployeeSystem.Infrastructure.Persistence.DataAccess;

public class TaskRepository : BaseRepository<EmployeeTask>, ITaskRepository
{
    public TaskRepository(SystemContext context) : base(context)
    {
    }
}