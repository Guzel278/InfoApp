using System.Linq;
using UserService.Models;

namespace UserService.Data
{
    public  class UserContextSeed
    {
        public static void Initialize(UserContext context)
        {
            context.Database.EnsureCreated();
            if (context.Users.Any())
                return;  //DB has been seeded

        }
  
    }
}
