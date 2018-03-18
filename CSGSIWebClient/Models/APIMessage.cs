using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Models
{
    public class APIMessage
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
