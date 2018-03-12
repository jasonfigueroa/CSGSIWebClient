using CSGSIWebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.ViewModels
{
    public class ProfileViewModel
    {
        public SteamPlayer SteamPlayer { get; set; }
        public List<CSMatch> CSMatchList { get; set; }
        public CSMatch LastMatch { get; set; }

        public float GetTotalKills() => CSMatchList.Sum(match => match.matchStats["kills"]);

        public float GetTotalDeaths() => CSMatchList.Sum(match => match.matchStats["deaths"]);

        public float GetKDR() => GetTotalKills() / GetTotalDeaths();

        public CSMatch GetLastMatch() => CSMatchList.Aggregate((match, nextMatch) => match.datetime_start > nextMatch.datetime_start ? match : nextMatch);
    }
}
