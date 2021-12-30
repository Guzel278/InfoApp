using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Models;
using UserService.Repositories;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserRepository userRepository;
        private readonly IUserCache cache;
        private readonly IUserCacheUpdate cacheUpdate;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IUserCache cache, IUserCacheUpdate cacheUpdate)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.cacheUpdate = cacheUpdate ?? throw new ArgumentNullException(nameof(cacheUpdate));
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUserInfo()
        {
            IEnumerable<UserModel> users = await cache.GetUsersCacheAsync(); 
            if (users == null)
            {
                users = await userRepository.GetUsersInfoAsync();
                await cache.SetUsersCacheAsync(users);              
            }           
            return users;
        }

        [HttpGet("{id}", Name = "GetUser")]
        //[Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<UserModel>> GetUserInfoById(int id)
        {
            UserModel user = await cache.GetUserCacheAsync(id);
            if (user == null)
            {
                user = await userRepository.GetUserInfoByIdAsync(id);
                if (user == null)
                {
                    logger.LogError($"User {id} not founded");
                    return NotFound();
                }
            }
            return Ok(user);
        }

        [HttpPut]
        //[Consumes("application/x-www-form-urlencoded")]
        public async Task<UserModel> SetUserStatus(int id, string status)
        {
            var user = await userRepository.GetUserInfoByIdAsync(id);
            user.Status = status;
            await userRepository.UpdateUserAsync(user);
            await cacheUpdate.UpdateCacheAsync();
            return user;
        }

        [HttpPost]
       // [Consumes("application/xml")]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel user)
        {
            await userRepository.CreateUserAsync(user);
            UserModel newuser = await userRepository.GetUserInfoByIdAsync(user.Id);
            if (newuser == null)
            {
                var result = new { ResponseSuccess = "false", ErrorId = "1" };
                return Ok(result);
            }
            var sresult = new { ResponseSuccess = "true", ErrorId = "0" };
            return Ok(sresult);            
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id)
        {
            UserModel user = await userRepository.GetUserInfoByIdAsync(id);
            if (user == null)
            {
                var result = new { ErrorId = 2, Msg = "User not found", Success = false };
                return Ok(result);
            }
            await userRepository.RemoveUserAsync(id);
            user.Status = "Deleted";
            var sresult = new { Msg = "User was removed", Success = true, user }; 
            return Ok(sresult);
        } 
    }
}
