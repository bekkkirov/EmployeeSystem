using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeSystem.Application.Interfaces.Services;
using EmployeeSystem.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeSystem.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly JwtOptions _tokenOptions;

    public TokenService(IOptions<JwtOptions> options)
    {
        _tokenOptions = options.Value;
    }

    public string GenerateToken(string userName)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userName),
        };

        var bytes = Encoding.UTF8.GetBytes(_tokenOptions.Key);
        var key = new SymmetricSecurityKey(bytes);

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(_tokenOptions.ExpiresInDays),
            SigningCredentials = credentials,
            Issuer = _tokenOptions.Issuer,
            Audience = _tokenOptions.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}