using System.Security.Claims;
using EmployeeSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace EmployeeSystem.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly ClaimsPrincipal _currentUser;

    public CurrentUserService(IHttpContextAccessor context)
    {
        _currentUser = context.HttpContext.User;
    }

    public string GetUserName()
    {
        return _currentUser.FindFirstValue(ClaimTypes.Name);
    }
}