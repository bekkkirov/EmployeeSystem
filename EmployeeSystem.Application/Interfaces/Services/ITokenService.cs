namespace EmployeeSystem.Application.Interfaces.Services;

public interface ITokenService
{
    public string GenerateToken(string userName);
}