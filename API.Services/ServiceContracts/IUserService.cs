using API.DataAccess.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.ServiceContracts
{
    public interface IUserService
    {
        Task<User> CreateUser();
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(Guid id);
        Task<User> GetUserById(Guid id);
    }
}
