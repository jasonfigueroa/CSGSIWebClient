using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Models
{
    public interface IMatchRepository
    {
        IEnumerable<CSMatch> GetAllMatches();
        CSMatch GetMatchById(int matchId);
    }
}
