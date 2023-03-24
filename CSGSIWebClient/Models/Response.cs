using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSGSIWebClient.Models
{
    public class Response
    {
        [JsonProperty("players")]
        public List<SteamPlayerResponse> Players { get; set; }
    }
}
