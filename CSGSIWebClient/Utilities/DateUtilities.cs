using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Utilities
{
    public class DateUtilities
    {
        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {

            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return origin.AddSeconds(timestamp);

        }
    }
}
