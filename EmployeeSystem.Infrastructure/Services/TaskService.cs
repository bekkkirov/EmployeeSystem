using AutoMapper;
using EmployeeSystem.Application.Interfaces.Persistence;
using EmployeeSystem.Application.Interfaces.Services;
using EmployeeSystem.Application.Models;

namespace EmployeeSystem.Infrastructure.Services;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public TaskService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }


    public async Task<IEnumerable<TaskDto>> GetCurrentTasksAsync()
    {
        var userName = _currentUserService.GetUserName();

        return await GetEmployeeTasksAsync(userName);
    }

    public async Task<IEnumerable<TaskDto>> GetEmployeeTasksAsync(string userName)
    {
        var tasks = await _unitOfWork.TaskRepository.GetEmployeeTasksAsync(userName);

        return _mapper.Map<IEnumerable<TaskDto>>(tasks);
    }
}