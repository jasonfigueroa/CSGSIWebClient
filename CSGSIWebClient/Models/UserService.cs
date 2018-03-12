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
        private SteamPlayer _steamPlayer;

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

            if (_steamPlayer == null)
            {
                InitializeSteamPlayer();
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

        private void InitializeSteamPlayer()
        {
            _steamPlayer = new SteamPlayer();
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

        public SteamPlayer GetSteamPlayer()
        {
            return _steamPlayer;
        }

        public void SetSteamPlayer(SteamPlayer steamPlayer)
        {
            _steamPlayer = steamPlayer;
        }

        public void Logout()
        {
            _user = null;
            _login = null;
            _steamId = null;
            _steamPlayer = null;

            InitializeUser();
            InitializeLogin();
            InitializeSteamId();
            InitializeSteamPlayer();
        }
    }
}
