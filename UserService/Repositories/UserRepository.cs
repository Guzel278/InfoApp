using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;

        public UserRepository(UserContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateUserAsync(UserModel user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserModel>> GetUsersInfoAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<UserModel> GetUserInfoByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<bool> RemoveUserAsync(int id)
        {
            UserModel user = await context.Users.FindAsync(id);                   
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserAsync(UserModel user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return true; 
        }
    }
}
