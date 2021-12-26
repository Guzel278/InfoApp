using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUserInfo()
        {
            return await userRepository.GetUsersInfoAsync();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserModel>> GetUserInfoById(int id)
        {
            var user = await userRepository.GetUserInfoByIdAsync(id);
            if (user == null)
            {
                logger.LogError($"User {id} not founded");
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut]
        public async Task<UserModel> SetUserStatus(int id, string status)
        {
            var user = await userRepository.GetUserInfoByIdAsync(id);
            user.Status = status;
            await userRepository.UpdateUserAsync(user);
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel user)
        {
            await userRepository.CreateUserAsync(user);
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
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
