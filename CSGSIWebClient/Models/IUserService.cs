using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Models
{
    public interface IUserService
    {
        User GetUser();
        void SetUser(User user);
        Login GetLoggedIn();
        void SetLoggedIn(Login login);
    }
}
