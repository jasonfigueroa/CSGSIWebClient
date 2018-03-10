using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Utilities
{
    public class MapUtilities
    {
        public Dictionary<string, string> MapDict { get; }

        public MapUtilities()
        {
            MapDict = new Dictionary<string, string>
            {
                { "de_cache", "Cache" },
                { "de_dust2", "Dust II" },
                { "de_inferno", "Inferno" },
                { "de_mirage", "Mirage" },
                { "de_cbble", "Cobblestone" },
                { "de_train", "Train" },
                { "de_overpass", "Overpass" },
                { "de_nuke", "Nuke" },
                { "de_canals", "Canals" },
                { "de_austria", "Austria" },
                { "de_shipped", "Shipped" }
            };
        }
    }
}
