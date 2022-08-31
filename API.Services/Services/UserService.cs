
using API.Services.ServiceContracts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {
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
