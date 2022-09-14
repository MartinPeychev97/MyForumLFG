using Services.Models.Requests;
using Services.Models.Responses;

namespace Services.Interfaces
{
    public interface IUserService
    {
        //Task<AuthenticateResponse> RegisterAsync(RegisterUserRequest request);
        Task<UserResponse> Create(CreateUserRequest request);
        Task<UserResponse> Update(Guid id, UpdateUserRequest request);
        Task Delete(Guid id);
        Task<UserResponse> GetUserById(Guid id);
        Task<IReadOnlyCollection<UserResponse>> GetAll();
    }
}
