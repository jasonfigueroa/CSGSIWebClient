using Newtonsoft.Json;

namespace CSGSIWebClient.Models
{
    public class RegisterRequest
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "steam_id")]
        public string SteamId { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
