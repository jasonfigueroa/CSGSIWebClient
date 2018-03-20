using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Models
{
    public interface IUserService
    {
        //User GetUser();
        //void SetUser(User user);
        //Login GetLogIn();
        //void SetLogIn(Login login);
        SteamId GetSteamId();
        void SetSteamId(SteamId steamId);
        //SteamPlayer GetSteamPlayer();
        //void SetSteamPlayer(SteamPlayer steamPlayer);
        //void Logout();
    }
}
