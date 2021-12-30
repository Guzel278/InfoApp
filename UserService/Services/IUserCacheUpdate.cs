using System.Threading.Tasks;

namespace UserService.Services
{
    public interface IUserCacheUpdate
    {
        Task UpdateCacheAsync();
    }
}
