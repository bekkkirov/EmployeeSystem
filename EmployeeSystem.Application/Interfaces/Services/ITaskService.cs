using EmployeeSystem.Application.Models;

namespace EmployeeSystem.Application.Interfaces.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetCurrentTasksAsync();

    Task<IEnumerable<TaskDto>> GetEmployeeTasksAsync(string userName);
}