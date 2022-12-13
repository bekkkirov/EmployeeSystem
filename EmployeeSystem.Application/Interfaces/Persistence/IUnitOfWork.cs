namespace EmployeeSystem.Application.Interfaces.Persistence;

public interface IUnitOfWork
{
    IEmployeeRepository EmployeeRepository { get; } 
    
    ITaskRepository TaskRepository { get; }

    Task SaveChangesAsync();
}