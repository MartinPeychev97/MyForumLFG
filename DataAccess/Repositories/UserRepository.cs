using API.DataAccess;
using AutoMapper;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Services.Models.Requests;
using Services.Models.Responses;
using System.Data;
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

        public async Task<BaseResponse> Create(CreateUserRequest request)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);

            if (existingUser is not null)
                throw new Exception("User with the email '" + request.Email + "' already exists");

            var user = mapper.Map<UserEntity>(request);

            string confirmEmailToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmedEmail = await userManager.ConfirmEmailAsync(user, confirmEmailToken);

            if (!confirmedEmail.Succeeded)
                throw new Exception("User email confirmation failed");

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new BaseResponse
                {
                    Code = result.Errors.First().Code,
                    Message = result.Errors.First().Description
                };

            return new BaseResponse()
            {
                Succees = result.Succeeded,
            };
        }

        public async Task Delete(string email)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            await userManager.DeleteAsync(user);
        }

        public async Task<IReadOnlyCollection<UserResponse>> GetAll()
        {
            var allUsers = await userManager.Users.ToListAsync();
            return (IReadOnlyCollection<UserResponse>)allUsers;
        }

        public async Task<UserResponse> GetUserById(string email)
        {
            var user = await userManager.Users
               .FirstOrDefaultAsync(e => e.Email == email);

            if (user is null)
                throw new Exception("The user you are looking for does not exist");

            var result = mapper.Map<UserResponse>(user);

            return result;
        }

        public async Task<BaseResponse> Update(UpdateUserRequest request)
        {
            var existingUser = GetUserById(request.Email);

            if (existingUser is null)
                throw new Exception("The user you are looking for does not exist");

            var user = mapper.Map<UserEntity>(request);
            var result = await userManager.UpdateAsync(user);


            if (!result.Succeeded)
                return new BaseResponse
                {
                    Code = result.Errors.First().Code,
                    Message = result.Errors.First().Description
                };

            return new BaseResponse()
            {
                Succees = result.Succeeded,
            };
        }

        public async Task<AuthenticateResponse> Login(LoginUserRequest userInput)
        {
            var result = await signInManager.PasswordSignInAsync(userInput.Email, userInput.Password, false, false);

            if (!result.Succeeded)
            {
                return new AuthenticateResponse()
                {
                    Unautorised = result.IsNotAllowed
                };
            }

            var generateToken = await GenerateJwtToken(userInput);

            return new AuthenticateResponse()
            {
                Token = generateToken
            };
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
