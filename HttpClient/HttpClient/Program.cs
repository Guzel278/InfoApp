using System;
using System.Threading.Tasks;

namespace HttpClientUser
{
    internal class Program
    {      
        static async Task Main(string[] args)
        {
            var client = new UserServiceClient();
            Console.WriteLine("Choose a method from the following list:\n1 CreateUser\n2 Get all Users Info\n3 Get User Info By Id\n4 Set User Status\n5 DeleteUser");
            if (!int.TryParse(Console.ReadLine(), out int method))
                return;
            int id;
            string status;
            switch (method)
            {
                case int when method == 1:
                    Console.WriteLine("input user id:");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine("input user name");
                    string name = Console.ReadLine();
                    Console.WriteLine("input user status");
                    status = Console.ReadLine();
                    await client.CreateUser(id, name, status);
                    break;
                case int when method == 2:                    
                     await client.GetUsers();
                    break;
                case int when method == 3:
                    Console.WriteLine("input user id:");
                    await client.GetUser(int.Parse(Console.ReadLine()));
                    break;
                case int when method == 4:
                    Console.WriteLine("input user id:");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine("input user status");
                    status = Console.ReadLine();
                    await client.SetUserStatus(id,status);
                    break;
                case int when method == 5:
                    Console.WriteLine("input user id:");
                    id = int.Parse(Console.ReadLine());
                    await client.DeleteUserStatus(id);
                    break;
            }
        }
    }
}
