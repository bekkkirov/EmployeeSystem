using EmployeeSystem.Domain.Entities;

namespace EmployeeSystem.Application.Models;

public class EmployeeDto
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public DateTime BirthDate { get; set; }

    public decimal Salary { get; set; }

    public string Post { get; set; } = default!;

    public List<TaskDto> AssignedTasks { get; set; } = new List<TaskDto>();
}