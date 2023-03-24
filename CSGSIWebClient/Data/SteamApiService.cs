using CSGSIWebClient.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSGSIWebClient.Data
{
    public class SteamApiService : ISteamApiService
    {
        private string _apiKey;
        private HttpClient _httpClient;
        private string _steamApiBaseUrl;

        public SteamApiService()
        {
            _apiKey = Environment.GetEnvironmentVariable("STEAM_API_KEY");
            _httpClient = new HttpClient();
            _steamApiBaseUrl = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002";
        }

        public SteamPlayerResponse GetSteamPlayer(SteamId steamId)
        {
            return GetSteamPlayerAsync(steamId).GetAwaiter().GetResult();
        }

        public async Task<SteamPlayerResponse> GetSteamPlayerAsync(SteamId steamId)
        {
            string url = $"{_steamApiBaseUrl}/?key={_apiKey}&steamids={steamId.Id}";

            HttpResponseMessage res = await _httpClient.GetAsync(url);
            HttpContent content = res.Content;

            string data = await content.ReadAsStringAsync();

            return JsonConvert
                .DeserializeObject<SteamPlayerResponseRootObject>(data)
                .Response
                .Players
                .FirstOrDefault();
        }
    }
}
