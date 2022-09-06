using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<UserEntity> userManager;
    private readonly IConfiguration configuration;
    private readonly SignInManager<UserEntity> signInManager;
    private readonly UserRepository userRepository;

    public UserRepository(UserManager<UserEntity> userManager, IConfiguration configuration, SignInManager<UserEntity> signInManager, UserRepository userRepository)
    {
        this.userManager = userManager;
        this.configuration = configuration;
        this.signInManager = signInManager;
        this.userRepository = userRepository;
    }
    public async Task<AuthenticateResponse> Login(LoginUserRequest userInput)
    {
        var result = await signInManager.PasswordSignInAsync(userInput.Email, userInput.Password, false, false);
        if (result.Succeeded)
        {
            var generateToken = userRepository.GenerateJwtToken(userInput);
            var token = new AuthenticateResponse(await generateToken);
            return token;
        }
        return null;
    }
    public async Task Logout()
    {
        await signInManager.SignOutAsync();
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
