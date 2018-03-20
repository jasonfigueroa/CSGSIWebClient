using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CSGSIWebClient.Data;
using CSGSIWebClient.Models;
using CSGSIWebClient.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSGSIWebClient.Controllers
{
    public class RegisterController : Controller
    {
        //private IUserService _userService;
        private User _user;
        private Register _register;
        private RegisterViewModel _registerViewModel;

        //public RegisterController(IUserService userService)
        public RegisterController()
        {
            //_userService = userService;
            _user = new User();
            _register = new Register();
            _registerViewModel = new RegisterViewModel();
        }

        public IActionResult Index()
        {
            return View(_registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                _register.Username = registerViewModel.Username;
                _register.SteamId = registerViewModel.SteamId;
                _register.Password = registerViewModel.Password;

                APIInterface.RegisterUser(_register);

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, _register.Username)
                    };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                //_user.username = _register.Username;
                //_user.password = _register.Password;

                //_userService.SetUser(_user);

                //_userService.SetLogIn(new Login { LoggedIn = true });

                //SteamId steamId = APIInterface.GetSteamId(_user);

                //_userService.SetSteamId(steamId);

                //List<SteamPlayer> playerList = SteamApiInterface.GetSteamPlayers(steamId);
                //SteamPlayer steamPlayer = playerList[0];

                //_userService.SetSteamPlayer(steamPlayer);

                return RedirectToAction("Index", "Matches");
            }
            return View(registerViewModel);
        }


    }
}
