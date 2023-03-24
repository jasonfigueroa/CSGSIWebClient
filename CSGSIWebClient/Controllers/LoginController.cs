using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSGSIWebClient.Models;
using Microsoft.AspNetCore.Mvc;
using CSGSIWebClient.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSGSIWebClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly IApiService _apiService;
        private readonly string _url;

        public LoginController(IApiService apiService, IConfiguration config)
        {
            _apiService = apiService;

            if (IsDevelopmentEnvironment())
            {
                _url = config["Kestrel:Endpoints:Http"];
            }
            else
            {
                _url = config["Kestrel:Endpoints:Https"];
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserLogin userLogin, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Username = userLogin.username,
                    Password = userLogin.password
                };

                JWT jwt = _apiService.LoginUser(user);

                if (jwt.AccessToken != null && jwt.RefreshToken != null)
                {
                    SteamId steamId = _apiService.GetSteamIdAsync(jwt).GetAwaiter().GetResult();

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, steamId.Id),
                        new Claim("AccessToken", jwt.AccessToken),
                        new Claim("RefreshToken", jwt.RefreshToken)
                    };

                    var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        // AllowRefresh = true, // Not sure if this is for JWT Refresh Tokens
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
                        IsPersistent = userLogin.stayLoggedIn ? true : false,
                        IssuedUtc = DateTime.Now
                    };

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect($"{returnUrl}?routedFromLogin=1");
                    }
                    else
                    {                        
                        return Redirect($"{_url}/Matches?routedFromLogin=1");
                    }
                }
            }

            return View(userLogin);
        }

        private bool IsDevelopmentEnvironment()
        {
            string currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return string.Equals("development", currentEnvironment, StringComparison.OrdinalIgnoreCase);
        }
    }
}
