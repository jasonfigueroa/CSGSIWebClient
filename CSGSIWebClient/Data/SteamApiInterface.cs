using CSGSIWebClient.Controllers;
using CSGSIWebClient.Models;
using Microsoft.AspNetCore.Http;
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
        //private static HttpContext _httpContext;

        //public SteamApiInterface(HttpContext httpContext)
        //{
        //    _httpContext = httpContext;
        //}

        public static List<SteamPlayer> GetSteamPlayers(SteamId steamId)
        {
            return GetSteamPlayersAsync(steamId).GetAwaiter().GetResult();
        }

        //public static List<SteamPlayer> GetSteamPlayers()
        //{
        //    string steamId = HttpContext.Session.GetString("_SteamId");
        //    //SteamId steamId = new SteamId { steam_id = _httpContext.Items["steamId"].ToString() };
        //    SteamId steamId = new SteamId { steam_id =  };
        //    return GetSteamPlayersAsync(steamId).GetAwaiter().GetResult();
        //    //return steamPlayers[0];
        //}

        private static async Task<List<SteamPlayer>> GetSteamPlayersAsync(SteamId steamId)
        {
            string url = $"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={_apiKey}&steamids={steamId.steam_id}";

            HttpClient client = new HttpClient();

            HttpResponseMessage res = await client.GetAsync(url);

            HttpContent content = res.Content;

            string data = await content.ReadAsStringAsync();

            var something = JsonConvert.DeserializeObject<RootObject>(data);

            return something.response.players;
        }
    }
}
