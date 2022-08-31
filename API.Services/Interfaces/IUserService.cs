using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.ServiceContracts
{
    public interface IUserService
    {
        Task<UserEntity> CreateUserAsync();
        Task<UserEntity> UpdateUserAsync(UserEntity user);
        Task<UserEntity> DeleteUserAsync(Guid id);
        Task<UserEntity> GetUserByIdAsync(Guid id);
    }
}
