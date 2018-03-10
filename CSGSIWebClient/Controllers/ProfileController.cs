using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSGSIWebClient.Data;
using CSGSIWebClient.Models;
using CSGSIWebClient.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSGSIWebClient.Controllers
{
    public class ProfileController : Controller
    {
        private IUserService _userService;
        private ProfileViewModel _profileViewModel;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
            _profileViewModel = new ProfileViewModel();
        }
        
        public IActionResult Index()
        {
            SteamApiInterface.GetSteamPlayer(_userService.GetSteamId());
            return View();
        }
    }
}
