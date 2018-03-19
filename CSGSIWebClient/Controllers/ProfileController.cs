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
        //private IUserService _userService;
        private User _user;
        private ProfileViewModel _profileViewModel;

        //public ProfileController(IUserService userService)
        public ProfileController()
        {
            //_userService = userService;
            _profileViewModel = new ProfileViewModel();
        }
        
        public IActionResult Index()
        {
            //if (_userService.GetLogIn().LoggedIn == false)
            //{
            //    return RedirectToAction("Index", "Login");
            //}

            //_user = _userService.GetUser();

            //SteamPlayer steamPlayer = _userService.GetSteamPlayer();

            //CSMatchList cSMatchList = APIInterface.GetCSMatches(_user);

            //_profileViewModel.SteamPlayer = steamPlayer;
            //_profileViewModel.CSMatchList = cSMatchList.Matches;

            return View(_profileViewModel);
        }
    }
}
