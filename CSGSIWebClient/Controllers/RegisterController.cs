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
        private Register _register;
        private RegisterViewModel _registerViewModel;

        public RegisterController()
        {
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
                        new Claim(ClaimTypes.Name, _register.SteamId)
                    };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Matches");
            }
            return View(registerViewModel);
        }


    }
}
