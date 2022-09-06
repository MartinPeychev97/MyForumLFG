using API.Services.ServiceContracts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Repositories;
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
        private readonly UserRepository userRepository;
        
        public UserService(IConfiguration configuration, SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, UserRepository userRepository)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userRepository = userRepository;
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
               var generateToken = userRepository.GenerateJwtToken(userInput);
               var token = new AuthenticateResponse(await generateToken);
               return token;
           }
            return null;
        }
        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
