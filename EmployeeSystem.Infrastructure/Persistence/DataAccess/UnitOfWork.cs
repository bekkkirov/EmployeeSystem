using EmployeeSystem.Application.Interfaces.Persistence;

namespace EmployeeSystem.Infrastructure.Persistence.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly SystemContext _context;

    public IEmployeeRepository EmployeeRepository { get; }

    public ITaskRepository TaskRepository { get; }

    public UnitOfWork(SystemContext context, IEmployeeRepository employeeRepository, ITaskRepository taskRepository)
    {
        _context = context;
        EmployeeRepository = employeeRepository;
        TaskRepository = taskRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}