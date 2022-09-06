using DataAccess.Entities;
using Services.Requests;
using Services.Responce;

namespace API.Services.ServiceContracts
{
    public interface IUserService
    {
        
        //Task<AuthenticateResponse> RegisterAsync(RegisterUserRequest request);
        void Create(CreateUserRequest request);
        void Update(Guid id, UpdateUserRequest request);
        void Delete(Guid id);
        UserEntity GetById(Guid id);
        IEnumerable<UserEntity> GetAll();
    }
}
