using Newtonsoft.Json;

namespace CSGSIWebClient.Models
{
    public class SteamPlayerResponseRootObject
    {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }
}
