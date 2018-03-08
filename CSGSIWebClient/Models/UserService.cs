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
        }

        public void InitializeUser()
        {
            _user = new User();
        }

        public void InitializeLogin()
        {
            _login = new Login { LoggedIn = false };
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

        public Login GetLoggedIn()
        {
            return _login;
        }

        public void SetLoggedIn(Login login)
        {
            _login.LoggedIn = login.LoggedIn;
        }
    }
}
