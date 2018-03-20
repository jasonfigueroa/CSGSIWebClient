using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CSGSIWebClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CSGSIWebClient.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSGSIWebClient.Controllers
{
    public class LoginController : Controller
    {
        private User _user;
        //private IUserService _userService;

        public LoginController()
        {
            _user = new User();
            //_userService = userService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_user);
        }

        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            if(ModelState.IsValid)
            {
                if (LoginUser(user.username, user.password))
                {
                    //_userService.SetUser(user);

                    //_userService.SetLogIn(new Login { LoggedIn = true });

                    SteamId steamId = APIInterface.GetSteamId(user);

                    //HttpContext.Session.SetString(SessionKeyName, steamId.steam_id);

                    //_userService.SetSteamId(steamId);

                    //List<SteamPlayer> playerList = SteamApiInterface.GetSteamPlayers(steamId);
                    //SteamPlayer steamPlayer = playerList[0];

                    //_userService.SetSteamPlayer(steamPlayer);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, steamId.steam_id),
                        //new Claim(ClaimTypes.UserData, steamId.steam_id)
                    };

                    var userIdentity = new ClaimsIdentity(claims, "login");

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "Matches");
                }
            }

            return View(user);
        }

        private bool LoginUser(string username, string password)
        {
            if (APIInterface.IsValidUser(new User { username = username, password = password }))
            {
                return true;
            }
            return false;
        }
    }
}
