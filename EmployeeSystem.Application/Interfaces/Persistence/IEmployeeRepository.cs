using EmployeeSystem.Domain.Entities;

namespace EmployeeSystem.Application.Interfaces.Persistence;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee?> GetByUserNameAsync(string userName);

    Task<Employee?> GetWithAssignedTasksAsync(string userName);
}