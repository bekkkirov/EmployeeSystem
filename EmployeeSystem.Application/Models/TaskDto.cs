using EmployeeSystem.Domain.Entities;
using TaskStatus = EmployeeSystem.Domain.Entities.TaskStatus;

namespace EmployeeSystem.Application.Models;

public class TaskDto
{
    public string Description { get; set; } = default!;

    public DateTime Created { get; set; }

    public DateTime CompleteBy { get; set; }

    public string Status { get; set; } = default!;
}