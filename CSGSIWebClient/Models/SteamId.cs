using Newtonsoft.Json;

namespace CSGSIWebClient.Models
{
    public class SteamId
    {
        [JsonProperty("steam_id")]
        public string Id { get; set; }
    }
}
