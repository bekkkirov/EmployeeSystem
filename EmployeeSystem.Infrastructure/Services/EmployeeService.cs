using AutoMapper;
using EmployeeSystem.Application.Exceptions;
using EmployeeSystem.Application.Interfaces.Persistence;
using EmployeeSystem.Application.Interfaces.Services;
using EmployeeSystem.Application.Models;

namespace EmployeeSystem.Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public EmployeeService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<EmployeeDto> GetCurrentProfileAsync()
    {
        var userName = _currentUserService.GetUserName();

        return await GetEmployeeProfileAsync(userName);
    }

    public async Task<EmployeeDto> GetEmployeeProfileAsync(string userName)
    {
        var employee = await _unitOfWork.EmployeeRepository.GetByUserNameAsync(userName);

        if (employee is null)
        {
            throw new NotFoundException("User with specified username was not found");
        }

        return _mapper.Map<EmployeeDto>(employee);
    }
}