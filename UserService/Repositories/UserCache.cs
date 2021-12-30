using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserCache : IUserCache
    {
        private readonly IDistributedCache cache;

        public UserCache(IDistributedCache cache)
        {
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<UserModel> GetUserCacheAsync(int id)
        {
            string userstring = await cache.GetStringAsync($"user-{id}");
            UserModel userModel = JsonConvert.DeserializeObject<UserModel>(userstring);
            return userModel;
        }

        public async Task<IEnumerable<UserModel>> GetUsersCacheAsync()
        {
            string userstring = await cache.GetStringAsync("users");
            IEnumerable<UserModel> userModels = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(userstring);
            return userModels;
        }

        public async Task SetUserCacheAsync(UserModel user)
        {
            string userstring = JsonConvert.SerializeObject(user);
            await cache.SetStringAsync($"user-{user.Id}", userstring);         
        }

        public async Task SetUsersCacheAsync(IEnumerable<UserModel> users)
        {
            string usersstr = JsonConvert.SerializeObject(users);
            await cache.SetStringAsync("users", usersstr);
        }
    }
}
