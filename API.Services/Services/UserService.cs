using API.Services.ServiceContracts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Requests;

namespace API.Services.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<UserEntity> signInManager;
        private readonly UserManager<UserEntity> userManager;
        public UserService(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public Task<UserEntity> CreateUserAsync()
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
    }
}
