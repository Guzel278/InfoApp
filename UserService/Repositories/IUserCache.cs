using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Repositories
{
    public interface IUserCache
    {
        Task<UserModel> GetUserCacheAsync(int id);
        Task<IEnumerable<UserModel>> GetUsersCacheAsync();
        Task SetUserCacheAsync(UserModel user);
        Task SetUsersCacheAsync(IEnumerable<UserModel> users);

    }
}
