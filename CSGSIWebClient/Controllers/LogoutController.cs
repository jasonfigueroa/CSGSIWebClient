using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSGSIWebClient.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSGSIWebClient.Controllers
{
    public class LogoutController : Controller
    {
        //private IUserService _userService;

        //public LogoutController(IUserService userService)
        //{
        //    _userService = userService;
        //}
        
        public IActionResult Index()
        {
            //_userService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
