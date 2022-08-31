using API.Services.ServiceContracts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Requests;
using Services.Responce;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<UserEntity> signInManager;
        private readonly UserManager<UserEntity> userManager;
        private readonly IConfiguration configuration;
        public UserService(IConfiguration configuration, SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> UpdateUserAsync(UserEntity user)
        {
            throw new NotImplementedException();
        }
        public async Task<AuthenticateResponse> LoginAsync(LoginUserRequest userInput)
        {
            var result = await signInManager.PasswordSignInAsync(userInput.Email, userInput.Password, false, false);
           if (result.Succeeded)
           {
               var generateToken = GenerateJwtToken(userInput);
               var token = new AuthenticateResponse(await generateToken);
               return token;
           }
            return null;
        }
        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        private async Task<string> GenerateJwtToken(LoginUserRequest user)
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
}
