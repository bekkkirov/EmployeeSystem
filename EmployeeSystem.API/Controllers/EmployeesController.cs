using EmployeeSystem.Application.Interfaces.Services;
using EmployeeSystem.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("current")]
    public async Task<ActionResult<EmployeeDto>> GetCurrentProfile()
    {
        var employee = await _employeeService.GetCurrentProfileAsync();

        return Ok(employee);
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeProfile(string userName)
    {
        var employee = await _employeeService.GetEmployeeProfileAsync(userName);

        return Ok(employee);
    }

}