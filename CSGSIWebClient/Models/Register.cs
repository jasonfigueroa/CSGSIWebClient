using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Models
{
    public class Register
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "steam_id")]
        public string SteamId { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
