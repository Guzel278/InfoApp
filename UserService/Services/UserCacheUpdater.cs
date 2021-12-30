using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Services
{
    public class UserCacheUpdater : IUserCacheUpdate
    {
        private readonly IUserCache userCache;
        private readonly IUserRepository userRepository;

        public UserCacheUpdater(IUserCache userCache, IUserRepository userRepository)
        {
            this.userCache = userCache ?? throw new ArgumentNullException(nameof(userCache));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task UpdateCacheAsync()
        {
            IEnumerable<UserModel> userModels = await userRepository.GetUsersInfoAsync();
            await userCache.SetUsersCacheAsync(userModels);
            foreach (UserModel userModel in userModels)
            {
                await userCache.SetUserCacheAsync(userModel);
            }
        }
    }
}
