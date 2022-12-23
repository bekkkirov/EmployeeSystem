using EmployeeSystem.Application.Interfaces.Services;
using EmployeeSystem.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSystem.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<ActionResult<string>> SignIn(SignInDto signInData)
    {
        var result = await _authenticationService.SignInAsync(signInData);

        return Ok(result);
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult<string>> SignUp(SignUpDto signUpData)
    {
        var result = await _authenticationService.SignUpAsync(signUpData);

        return Ok(result);
    }
}