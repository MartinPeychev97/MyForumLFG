using Services.Models.Requests;
using Services.Models.Responses;

namespace Services.Interfaces;

public interface IUserRepository
{
    Task<BaseResponse> Create(CreateUserRequest request);
    Task<BaseResponse> Update(UpdateUserRequest request);
    Task Delete(string email);
    Task<UserResponse> GetUserById(string email);
    Task<IReadOnlyCollection<UserResponse>> GetAll();
    Task<AuthenticateResponse> Login(LoginUserRequest userInput);
    Task Logout();
}