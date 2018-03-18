using CSGSIWebClient.Models;
using CSGSIWebClient.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSGSIWebClient.Data
{
    public class APIInterface
    {
        private IUserService _userService;

        public APIInterface(IUserService userService)
        {
            _userService = userService;
        }

        public static bool IsValidUser(User user)
        {
            JWT jwt = Auth(user).GetAwaiter().GetResult();
            if (jwt.access_token == null)
            {
                return false;
            }
            return true;
        }

        private static async Task<JWT> Auth(User user)
        {
            string url = "https://api.jasonfigueroa.io/auth";

            using (HttpClient client = new HttpClient())

            using (HttpResponseMessage res = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<JWT>(data);
            }
        }

        public static CSMatch GetCSMatch(User user, int matchId)
        {
            JWT jwt = Auth(user).GetAwaiter().GetResult();
            return GetCSMatchAsync(jwt, matchId).GetAwaiter().GetResult();
        }

        private static async Task<CSMatch> GetCSMatchAsync(JWT jwt, int matchId)
        {
            string url = $"https://api.jasonfigueroa.io/match/{matchId}";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", $"JWT {jwt.access_token}");        

            HttpResponseMessage res = await client.GetAsync(url);

            HttpContent content = res.Content;

            string data = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CSMatch>(data);
        }

        public static CSMatchList GetCSMatches(User user)
        {
            JWT jwt = Auth(user).GetAwaiter().GetResult();
            return GetCSMatchesAsync(jwt).GetAwaiter().GetResult();
        }

        private static async Task<CSMatchList> GetCSMatchesAsync(JWT jwt)
        {
            string url = $"https://api.jasonfigueroa.io/match/list";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", $"JWT {jwt.access_token}");

            HttpResponseMessage res = await client.GetAsync(url);

            HttpContent content = res.Content;

            string data = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CSMatchList>(data);
        }

        public static SteamId GetSteamId(User user)
        {
            JWT jwt = Auth(user).GetAwaiter().GetResult();
            return GetSteamIdAsync(jwt).GetAwaiter().GetResult();
        }

        private static async Task<SteamId> GetSteamIdAsync(JWT jwt)
        {
            string url = "https://api.jasonfigueroa.io/user/steamid";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", $"JWT {jwt.access_token}");

            HttpResponseMessage res = await client.GetAsync(url);

            HttpContent content = res.Content;

            string data = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SteamId>(data);
        }

        public static void RegisterUser(Register register)
        {
            RegisterUserAsync(register).GetAwaiter().GetResult();
        }

        private static async Task RegisterUserAsync(Register register)
        {
            string url = "https://api.jasonfigueroa.io/register";

            HttpClient client = new HttpClient();

            HttpResponseMessage res = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json"));
        }

        public static async Task<APIMessage> IsUsernameInDb(string username)
        {
            string url = $"https://api.jasonfigueroa.io/usernameexists/{username}";

            HttpClient client = new HttpClient();

            HttpResponseMessage res = await client.GetAsync(url);

            HttpContent content = res.Content;

            string data = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<APIMessage>(data);
        }

        public static async Task<APIMessage> IsSteamIdInDb(string steamId)
        {
            string url = $"https://api.jasonfigueroa.io/steamidexists/{steamId}";

            HttpClient client = new HttpClient();

            HttpResponseMessage res = await client.GetAsync(url);

            HttpContent content = res.Content;

            string data = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<APIMessage>(data);
        }
    }
}
