using System;
using Newtonsoft.Json;
//using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace HttpClientUser
{
    public class InfoMethods
    {
        static HttpClient client = new HttpClient();
        public static  async Task<string> GetUsers()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:5000/api/User"),
                Method = HttpMethod.Get             
            };
            var GetResponse = await client.SendAsync(request);
            var result = new { ResultCode = GetResponse.StatusCode, Content = await GetResponse.Content.ReadAsStringAsync() };
            return JsonConvert.SerializeObject(result);
        }
        public static async Task<string> GetUser(int id)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"http://localhost:5000/api/User?id={id}"),
                Method = HttpMethod.Get
            };
            var GetResponse = await client.SendAsync(request);
            var result = new { ResultCode = GetResponse.StatusCode, Content = await GetResponse.Content.ReadAsStringAsync() };
            return JsonConvert.SerializeObject(result);
        }
        public static async Task<string> CreateUser(int id, string name, string status)
        {            
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"http://localhost:5000/api/User?id={id}&name?={name}&status={status}"),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    Id = id,
                    Name = name,
                    Status = status
                }))
            };
            var GetResponse = await client.SendAsync(request);
            var result = new { ResultCode = GetResponse.StatusCode, Content = await GetResponse.Content.ReadAsStringAsync() };
            return JsonConvert.SerializeObject(result);
        }
        public static async Task<string> SetUserStatus(int id, string status)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"http://localhost:5000/api/User?id={id}&status={status}"),
                Method = HttpMethod.Put              
            };
            var GetResponse = await client.SendAsync(request);
            var result = new { ResultCode = GetResponse.StatusCode, Content = await GetResponse.Content.ReadAsStringAsync() };
            return JsonConvert.SerializeObject(result);
        }
        public static async Task<string> DeleteUserStatus(int id)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"http://localhost:5000/api/User?id={id}"),
                Method = HttpMethod.Delete
            };
            var GetResponse = await client.SendAsync(request);
            var result = new { ResultCode = GetResponse.StatusCode, Content = await GetResponse.Content.ReadAsStringAsync() };
            return JsonConvert.SerializeObject(result);
        }
    }
}
