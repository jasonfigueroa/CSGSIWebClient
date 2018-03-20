using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Models
{
    public class SteamId
    {
        //public static readonly object SteamIdKey = new Object();

        //public void Invoke(HttpContext httpContext, string steamId)
        //{
        //    httpContext.Items[SteamIdKey] = steamId;
        //}
        public string steam_id { get; set; }
    }
}
