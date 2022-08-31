using DataAccess.Entities;
using Services.Requests;

namespace API.Services.ServiceContracts
{
    public interface IUserService
    {
        Task<AuthenticateResponse> LoginAsync(LoginUserRequest userInput);
        Task LogoutAsync();
        //Task<AuthenticateResponse> RegisterAsync(RegisterUserRequest request);
        Task<UserEntity> CreateUserAsync();
        Task<UserEntity> UpdateUserAsync(UserEntity user);
        Task<UserEntity> DeleteUserAsync(Guid id);
        Task<UserEntity> GetUserByIdAsync(Guid id);
    }
}
