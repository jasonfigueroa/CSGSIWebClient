using CSGSIWebClient.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSGSIWebClient.Data
{
    public class ApiService : IApiService
    {
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;

        public ApiService(IConfiguration configuration)
        {
            _apiUrl = configuration["Api:Url"];
            _httpClient = new HttpClient();
        }

        public JWT LoginUser(User user)
        {
            return Auth(user).GetAwaiter().GetResult();
        }

        private async Task<JWT> Auth(User user)
        {
            string url = $"{_apiUrl}/login";

            HttpClient client = new HttpClient();

            using (HttpResponseMessage res = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<JWT>(data);
            }
        }

        public async Task<SteamId> GetSteamIdAsync(JWT jwt)
        {
            string url = $"{_apiUrl}/user/steamid";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt.access_token}");

            HttpResponseMessage res = await client.GetAsync(url);

            HttpContent content = res.Content;

            string data = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SteamId>(data);
        }

        public void RegisterUser(RegisterRequest request)
        {
            bool usernameInDb = IsUsernameInDb(request.Username);
            bool steamIdInDb = IsSteamIdInDb(request.SteamId);

            if (!usernameInDb && !steamIdInDb)
            {
                RegisterUserAsync(request).GetAwaiter().GetResult();
            }
        }

        private async Task RegisterUserAsync(RegisterRequest request)
        {
            string url = $"{_apiUrl}/register";

            HttpClient client = new HttpClient();

            HttpResponseMessage res = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
        }

        public bool IsUsernameInDb(string username)
        {
            string url = $"{_apiUrl}/usernameexists/{username}";

            HttpClient client = new HttpClient();

            HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

            HttpContent content = res.Content;

            string data = content.ReadAsStringAsync().GetAwaiter().GetResult();

            string message = JsonConvert.DeserializeObject<APIMessage>(data).Message;

            return message == "User with that username already exists.";
        }

        public bool IsSteamIdInDb(string steamId)
        {
            string url = $"{_apiUrl}/steamidexists/{steamId}";

            HttpClient client = new HttpClient();

            HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

            HttpContent content = res.Content;

            string data = content.ReadAsStringAsync().GetAwaiter().GetResult();

            string message = JsonConvert.DeserializeObject<APIMessage>(data).Message;
            
            return message == "User with that steam id already exists.";
        }
    }
}
