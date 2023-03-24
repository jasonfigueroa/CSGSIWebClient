using Newtonsoft.Json;

namespace CSGSIWebClient.Models
{
    public class SteamPlayerResponse
    {
        [JsonProperty(PropertyName = "personaname")]
        public string PersonaName { get; set; }

        [JsonProperty(PropertyName = "avatar")]
        public string Avatar { get; set; }

        [JsonProperty(PropertyName = "avatarfull")]
        public string AvatarFull { get; set; }
    }
}
