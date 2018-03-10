using CSGSIWebClient.Models;
using CSGSIWebClient.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSGSIWebClient.ViewModels
{
    public class HomeViewModel
    {
        public string Title { get; set; }

        [JsonProperty(PropertyName = "matches")]
        public List<CSMatch> Matches { get; set; }

        public MapUtilities mapUtilities = new MapUtilities();
        public TeamUtilities teamUtilities = new TeamUtilities();
    }
}
