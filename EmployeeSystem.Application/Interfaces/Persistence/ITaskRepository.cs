using EmployeeSystem.Domain.Entities;

namespace EmployeeSystem.Application.Interfaces.Persistence;

public interface ITaskRepository : IRepository<EmployeeTask>
{
    Task<IEnumerable<EmployeeTask>> GetEmployeeTasksAsync(string userName);
}