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

        public CSMatch HighestScoreMatch() => CSMatchList.Aggregate((match, nextMatch) => match.matchStats["score"] > nextMatch.matchStats["score"] ? match : nextMatch);


        public CSMatch BestKDRMatch()
        {
            int i;

            CSMatch highestKDRMatch = CSMatchList[0];

            for (i = 0; i < CSMatchList.Count; i++)
            {
                CSMatch currentMatch = CSMatchList[i];

                float highestKDRMatchKills = highestKDRMatch.matchStats["kills"];
                float highestKDRMatchDeaths = highestKDRMatch.matchStats["deaths"];

                float currentMatchKills = currentMatch.matchStats["kills"];
                float currentMatchDeaths = currentMatch.matchStats["deaths"];

                if (GetKDR(highestKDRMatchKills, highestKDRMatchDeaths) < GetKDR(currentMatchKills, currentMatchDeaths))
                {
                    highestKDRMatch = currentMatch;
                }
            }
            return highestKDRMatch;
        }

        private float GetKDR(float kills, float deaths)
        {
            if (deaths == 0 && kills > deaths)
            {
                return kills / 1;
            }

            return kills / deaths;
        }
    }
}
