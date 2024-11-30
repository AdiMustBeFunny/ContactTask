using Contacto.Domain.Abstractions;
using Contacto.Domain.Abstractions.EntityService;
using Contacto.Domain.Entities;
using Contacto.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Contacto.DomainService.EntityService;

public class ApplicationUserService : IApplicationUserService
{
    private readonly IPasswordService _passwordService;
    private readonly IConfiguration _configuration;

    public ApplicationUserService(IPasswordService passwordService, IConfiguration configuration)
    {
        _passwordService = passwordService;
        _configuration = configuration;
    }

    /// <summary>
    /// Function returns a jwt token as string
    /// </summary>
    public Result<string> AuthenticateUser(ApplicationUser applicationUser, string password)
    {
        var passwordVerifyResult = _passwordService.VerifyPassword(password, applicationUser.PasswordHash, applicationUser.PasswordSalt);

        if (passwordVerifyResult.IsFailure) 
        {
            return Result.Failure<string>(passwordVerifyResult.Error);
        }

        string secretKey = _configuration["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.Username)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = credentials,
            Issuer = _configuration["Jwt:Issuer"]!,
            Audience = _configuration["Jwt:Audience"]!
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}
