using EmployeeSystem.Application.Models;

namespace EmployeeSystem.Application.Interfaces.Services;

public interface IAuthenticationService
{
    Task<string> SignInAsync(SignInDto signInData);

    Task<string> SignUpAsync(SignUpDto signUpData);
}