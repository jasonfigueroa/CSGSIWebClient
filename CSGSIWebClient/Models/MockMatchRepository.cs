using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Models
{
    public class MockMatchRepository: IMatchRepository
    {
        private List<CSMatch> _matches;

        public MockMatchRepository()
        {
            if (_matches == null)
            {
                InitializeMatches();
            }
        }

        private void InitializeMatches()
        {
            MatchStats matchStats1 = new MatchStats { id = 1, match_id = 1, kills = 10, assists = 1, deaths = 7, mvps = 2, score = 26 };
            MatchStats matchStats2 = new MatchStats { id = 2, match_id = 2, kills = 16, assists = 2, deaths = 9, mvps = 3, score = 37 };
            MatchStats matchStats3 = new MatchStats { id = 3, match_id = 3, kills = 5, assists = 3, deaths = 7, mvps = 1, score = 19 };
            _matches = new List<CSMatch>
            {
                //new CSMatch {id = 1, user_id = 1, datetime_start = 1520306680634, minutes_played = 22, map_name = "de_dust2", team = "T", round_win_team = "T", matchStats = matchStats1},
                //new CSMatch {id = 2, user_id = 1, datetime_start = 1520309112, minutes_played = 28, map_name = "de_dust2", team = "T", round_win_team = "T", matchStats = matchStats2},
                //new CSMatch {id = 3, user_id = 1, datetime_start = 1520311061, minutes_played = 21, map_name = "de_dust2", team = "T", round_win_team = "T", matchStats = matchStats3}
            };
        }

        public IEnumerable<CSMatch> GetAllMatches()
        {
            return _matches;
        }

        public CSMatch GetMatchById(int matchId)
        {
            return _matches.FirstOrDefault(m => m.id == matchId);
        }
    }
}
