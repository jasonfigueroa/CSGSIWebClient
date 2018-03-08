using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSGSIWebClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSGSIWebClient.Controllers
{
    public class LoginController : Controller
    {
        private IUserService _userService;
        private User _user;

        public LoginController(IUserService userService)
        {
            _userService = userService;

            if (_userService.GetUser() == null)
            {
                _user = new User();
            }

            _user = _userService.GetUser();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_user);
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if(ModelState.IsValid)
            {
                _userService.SetUser(user);
                return RedirectToAction("SuccessfulLogin");
            }

            return View(user);
        }

        public IActionResult SuccessfulLogin()
        {
            // to control user routing
            if (_userService.GetLoggedIn().LoggedIn == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
