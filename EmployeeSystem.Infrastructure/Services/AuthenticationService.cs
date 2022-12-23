using AutoMapper;
using EmployeeSystem.Application.Exceptions;
using EmployeeSystem.Application.Interfaces.Persistence;
using EmployeeSystem.Application.Interfaces.Services;
using EmployeeSystem.Application.Models;
using EmployeeSystem.Domain.Entities;
using EmployeeSystem.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeeSystem.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly UserManager<UserIdentity> _userManager;
    private readonly SignInManager<UserIdentity> _signInManager;

    public AuthenticationService(IUnitOfWork unitOfWork, ITokenService tokenService,
                                 IMapper mapper, UserManager<UserIdentity> userManager,
                                 SignInManager<UserIdentity> signInManager)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }


    public async Task<string> SignInAsync(SignInDto signInData)
    {
        var userIdentity = await _userManager.FindByNameAsync(signInData.UserName);

        if (userIdentity is null)
        {
            throw new IdentityException("Employee with specified username doesn't exist.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(userIdentity, signInData.Password, false);

        if (!result.Succeeded)
        {
            throw new IdentityException("Invalid password.");
        }

        return _tokenService.GenerateToken(userIdentity.UserName);
    }

    public async Task<string> SignUpAsync(SignUpDto signUpData)
    {
        var identityToAdd = new UserIdentity() { UserName = signUpData.UserName, Email = signUpData.Email };

        var result = await _userManager.CreateAsync(identityToAdd, signUpData.Password);

        if (!result.Succeeded)
        {
            throw new IdentityException(result.Errors.FirstOrDefault()?.Description ?? "Sign up failed.");
        }

        var employeeToAdd = _mapper.Map<Employee>(signUpData);

        _unitOfWork.EmployeeRepository.Add(employeeToAdd);
        await _unitOfWork.SaveChangesAsync();

        return _tokenService.GenerateToken(signUpData.UserName);
    }
}