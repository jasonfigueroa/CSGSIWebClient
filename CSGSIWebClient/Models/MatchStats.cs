using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Models
{
    public class MatchStats
    {
        public int id { get; set; }
        public int match_id { get; set; }
        public int kills { get; set; }
        public int assists { get; set; }
        public int deaths { get; set; }
        public int mvps { get; set; }
        public int score { get; set; }
    }
}
