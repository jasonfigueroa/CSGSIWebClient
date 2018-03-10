using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Models
{
    public class UserService : IUserService
    {
        private User _user;
        private Login _login;
        private SteamId _steamId;

        public UserService()
        {
            if (_user == null)
            {
                InitializeUser();
            }

            if (_login == null)
            {
                InitializeLogin();
            }

            if (_steamId == null)
            {
                InitializeSteamId();
            }
        }

        private void InitializeUser()
        {
            _user = new User();
        }

        private void InitializeLogin()
        {
            _login = new Login { LoggedIn = false };
        }

        private void InitializeSteamId()
        {
            _steamId = new SteamId();
        }

        public User GetUser()
        {
            return _user;
        }

        public void SetUser(User user)
        {
            _user.username = user.username;
            _user.password = user.password;
        }

        public Login GetLogIn()
        {
            return _login;
        }

        public void SetLogIn(Login login)
        {
            _login.LoggedIn = login.LoggedIn;
        }

        public SteamId GetSteamId()
        {
            return _steamId;
        }

        public void SetSteamId(SteamId steamId)
        {
            _steamId.steam_id = steamId.steam_id;
        }

        public void Logout()
        {
            _user = null;
            _login = null;
            _steamId = null;

            InitializeUser();
            InitializeLogin();
            InitializeSteamId();
        }
    }
}
