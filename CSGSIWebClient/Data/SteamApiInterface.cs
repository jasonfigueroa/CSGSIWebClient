using CSGSIWebClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSGSIWebClient.Data
{
    public class SteamApiInterface
    {
        private static string _apiKey = "E821014121563F86283961754BAC0C1C";

        public static SteamPlayer GetSteamPlayer(SteamId steamId)
        {
            return GetSteamPlayerAsync(steamId).GetAwaiter().GetResult();
        }

        private static async Task<SteamPlayer> GetSteamPlayerAsync(SteamId steamId)
        {
            string url = $"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={_apiKey}&steamids={steamId.steam_id}";

            HttpClient client = new HttpClient();

            HttpResponseMessage res = await client.GetAsync(url);

            HttpContent content = res.Content;

            string data = await content.ReadAsStringAsync();

            var something = JsonConvert.DeserializeObject<RootObject>(data);

            return new SteamPlayer();
        }
    }
}
