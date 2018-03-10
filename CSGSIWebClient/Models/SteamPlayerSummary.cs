using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Models
{
    public class SteamPlayerSummary
    {
        [JsonProperty(PropertyName = "response")]
        public Dictionary<string, Dictionary<string, List<SteamPlayer>>> Response { get; set; }
    }
}
