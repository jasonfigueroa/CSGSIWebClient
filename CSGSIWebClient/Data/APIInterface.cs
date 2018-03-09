﻿using CSGSIWebClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSGSIWebClient.Data
{
    public class APIInterface
    {
        private IUserService _userService;

        public APIInterface(IUserService userService)
        {
            _userService = userService;
        }

        public static bool IsValidUser(User user)
        {
            JWT jwt = Auth(user).GetAwaiter().GetResult();
            if (jwt.access_token == null)
            {
                return false;
            }
            return true;
        }

        static async Task<JWT> Auth(User user)
        {
            string url = "http://localhost:5000/auth";

            using (HttpClient client = new HttpClient())

            using (HttpResponseMessage res = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<JWT>(data);
            }
        }

        public static CSMatch GetCSMatch(User user, int matchId)
        {
            JWT jwt = Auth(user).GetAwaiter().GetResult();
            return GetCSMatchAsync(jwt, matchId).GetAwaiter().GetResult();
        }

        private static async Task<CSMatch> GetCSMatchAsync(JWT jwt, int matchId)
        {
            string url = $"http://localhost:5000/match/{matchId}";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", $"JWT {jwt.access_token}");        

            HttpResponseMessage res = await client.GetAsync(url);

            HttpContent content = res.Content;

            string data = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CSMatch>(data);
        }
    }
}
