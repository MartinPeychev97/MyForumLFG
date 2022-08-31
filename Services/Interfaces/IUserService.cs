using DataAccess.Entities;
using Services.Requests;
using Services.Responce;

namespace API.Services.ServiceContracts
{
    public interface IUserService
    {
        Task<AuthenticateResponse> LoginAsync(LoginUserRequest userInput);
        Task LogoutAsync();
        //Task<AuthenticateResponse> RegisterAsync(RegisterUserRequest request);
        Task<CreateUserResponse> CreateUser(CreateUserRequest request);
        Task<UserEntity> UpdateUserAsync(UserEntity user);
        Task<UserEntity> DeleteUserAsync(Guid id);
        Task<UserEntity> GetUserByIdAsync(Guid id);
    }
}
