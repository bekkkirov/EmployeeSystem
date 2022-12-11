namespace EmployeeSystem.Domain.Entities;

public class Manager : Employee
{
    public List<EmployeeTask> CreatedTasks { get; set; } = new List<EmployeeTask>();
}