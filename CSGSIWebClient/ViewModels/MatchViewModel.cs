using CSGSIWebClient.Models;
using CSGSIWebClient.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.ViewModels
{
    public class MatchViewModel
    {
        public string Title { get; set; }

        //[JsonProperty(PropertyName = "match")]
        public CSMatch Match { get; set; }

        public MapUtilities mapUtilities = new MapUtilities();
        public TeamUtilities teamUtilities = new TeamUtilities();

        public float GetKDR() => (float)Match.matchStats["kills"] / Match.matchStats["deaths"];
    }
}
