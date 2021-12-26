using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetUsersInfoAsync();
        Task<UserModel> GetUserInfoByIdAsync(int id);
    
        Task CreateUserAsync(UserModel user);
        Task<bool> UpdateUserAsync(UserModel user);
        Task<bool> RemoveUserAsync(int id);
    }
}
