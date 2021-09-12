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
using Microsoft.AspNetCore.Authentication.Cookies;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSGSIWebClient.Controllers
{
    public class LoginController : Controller
    {
        private UserLogin _userLogin;

        public LoginController()
        {
            _userLogin = new UserLogin(); // not sure if this is used anywhere
        }

        public IActionResult Index()
        {
            return View(_userLogin);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserLogin userLogin, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    username = userLogin.username,
                    password = userLogin.password
                };

                if (LoginUser(user.username, user.password))
                {
                    SteamId steamId = APIInterface.GetSteamId(user); // fix the casing on the User properties

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, steamId.steam_id)
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
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Matches");
                    }
                }
            }

            return View(userLogin);
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
