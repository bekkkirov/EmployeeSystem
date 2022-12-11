namespace EmployeeSystem.Domain.Entities;

public class EmployeeTask : BaseEntity
{
    public string Description { get; set; } = default!;

    public DateTime Created { get; set; }

    public DateTime CompleteBy { get; set; }

    public TaskStatus Status { get; set; }


    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = default!;

    public int ManagerId { get; set; }
    public Manager Manager { get; set; } = default!;
}