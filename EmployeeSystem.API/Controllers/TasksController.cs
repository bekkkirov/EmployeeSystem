using EmployeeSystem.Application.Interfaces.Services;
using EmployeeSystem.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("current")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetCurrentEmployeeTasks()
    {
        var tasks = await _taskService.GetCurrentTasksAsync();

        return Ok(tasks);
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetEmployeeTasks(string userName)
    {
        var tasks = await _taskService.GetEmployeeTasksAsync(userName);

        return Ok(tasks);
    }
}