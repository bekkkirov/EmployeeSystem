using EmployeeSystem.Application.Models;

namespace EmployeeSystem.Application.Interfaces.Services;

public interface IEmployeeService
{
    Task<EmployeeDto> GetCurrentProfileAsync();

    Task<EmployeeDto> GetEmployeeProfileAsync(string userName);
}