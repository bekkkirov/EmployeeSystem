namespace EmployeeSystem.Domain.Entities;

public class Employee : BaseEntity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public DateTime BirthDate { get; set; }

    public decimal Salary { get; set; }

    public Post Post { get; set; }

    public string Discriminator { get; set; } = default!;


    public List<EmployeeTask> AssignedTasks { get; set; } = new List<EmployeeTask>();
}