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
        private readonly IApiService _apiService;
        private readonly ISteamApiService _steamApiService;

        public RegisterController(IApiService apiService, ISteamApiService steamApiService)
        {
            _apiService = apiService;
            _steamApiService = steamApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel registerViewModel)
        {
            bool isSteamIdInDb = _apiService.IsSteamIdInDb(registerViewModel.SteamId);
            bool isExistingUsername = _apiService.IsUsernameInDb(registerViewModel.Username);

            var steamId = new SteamId
            {
                Id = registerViewModel.SteamId
            };

            var steamPlayer = _steamApiService.GetSteamPlayerAsync(steamId).GetAwaiter().GetResult();

            bool isExistingSteamPlayer = steamPlayer != null;

            // Model validation
            if (ModelState.IsValid)
            {
                // Data validation
                if (!isSteamIdInDb && !isExistingUsername && isExistingSteamPlayer)
                {
                    var request = new RegisterRequest
                    {
                        Username = registerViewModel.Username,
                        SteamId = registerViewModel.SteamId,
                        Password = registerViewModel.Password
                    };

                    _apiService.RegisterUser(request);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, request.SteamId)
                    };

                    var userIdentity = new ClaimsIdentity(claims, "login");

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "Matches");
                }
            }

            return View(registerViewModel);
        }


    }
}
