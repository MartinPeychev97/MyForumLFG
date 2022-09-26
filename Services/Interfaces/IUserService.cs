using Services.Models.Requests;
using Services.Models.Responses;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse> Create(CreateUserRequest request);
        Task<BaseResponse> Update(UpdateUserRequest request);
        Task Delete(string email);
        Task<UserResponse> GetUserById(string emaild);
        Task<IReadOnlyCollection<UserResponse>> GetAll();
    }
}
