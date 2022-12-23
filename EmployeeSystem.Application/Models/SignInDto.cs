namespace EmployeeSystem.Application.Models;

public class SignInDto
{
    public string UserName { get; set; } = default!;

    public string Password { get; set; } = default!;
}