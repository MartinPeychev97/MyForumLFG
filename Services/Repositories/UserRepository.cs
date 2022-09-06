using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Requests;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories;

public class UserRepository
{
    private readonly UserManager<UserEntity> userManager;
    private readonly IConfiguration configuration;

    public UserRepository(UserManager<UserEntity> userManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.configuration = configuration;

    }
    public async Task<string> GenerateJwtToken(LoginUserRequest user)
    {
        var claims = new List<Claim>
        {
            new Claim("email", user.Email.ToString()),
        };

        var userRole = await userManager.FindByEmailAsync(user.Email);
        var roles = await userManager.GetRolesAsync(userRole);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecret = configuration.GetSection("AppSettings").GetSection("Secret");
        var key = Encoding.UTF8.GetBytes(jwtSecret.Value);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = configuration.GetSection("AppSettings").GetSection("Issuer").Value,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
