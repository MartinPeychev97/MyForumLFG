using API.DataAccess;
using AutoMapper;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Services.Models.Requests;
using Services.Models.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserManager<UserEntity> userManager;
        private IConfiguration configuration;
        private SignInManager<UserEntity> signInManager;
        private DBContext context;
        private readonly IMapper mapper;

        public UserRepository(DBContext context, 
            UserManager<UserEntity> userManager, 
            IConfiguration configuration, 
            SignInManager<UserEntity> signInManager,
            IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task<UserResponse> Create(CreateUserRequest request)
        {
            var user = context.Users.Where(x => x.Email.Equals(request.Email)).ToList();
            
            if (user is not null)
                throw new Exception("User with the email '" + request.Email + "' already exists");

            // map model to new user object
            //Which to custom mapper
            var userEntity = mapper.Map<UserEntity>(request);

            // hash password
            userEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userEntity.PasswordHash);

            // save user
            await context.Users.AddAsync(userEntity);
            await context.SaveChangesAsync();

            return new UserResponse();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<UserResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> Update(Guid id, UpdateUserRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticateResponse> Login(LoginUserRequest userInput)
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
}
