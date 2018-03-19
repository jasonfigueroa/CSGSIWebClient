using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSGSIWebClient.Data;
using CSGSIWebClient.Models;
using CSGSIWebClient.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSGSIWebClient.Controllers
{
    public class MatchesController : Controller
    {
        //private readonly IUserService _userService;
        //private User _user;

        //public MatchesController(IUserService userService)
        public MatchesController()
        {
            //_userService = userService;
            //_user = userService.GetUser();
        }
        [Authorize]
        public IActionResult Index()
        {
            //if (_userService.GetLogIn().LoggedIn == false)
            //{
            //    return RedirectToAction("Index", "Login");
            //}

            //CSMatchList cSMatchList = APIInterface.GetCSMatches(_user);
            MatchesViewModel matchesViewModel = new MatchesViewModel()
            {
                Title = "Matches",
                //Matches = cSMatchList.Matches
            };

            return View(matchesViewModel);
        }

        [Authorize]
        public IActionResult Match(int id)
        {
            //if (_userService.GetLogIn().LoggedIn == false)
            //{
            //    return RedirectToAction("Index", "Login");
            //}

            MatchViewModel matchViewModel = new MatchViewModel()
            {
                Title = "Game Details",
                //Match = APIInterface.GetCSMatch(_user, id)
                //Match = APIInterface.GetCSMatch(id)
            };

            //if (matchViewModel.Match == null)
            //{
            //    return NotFound();
            //}

            return View(matchViewModel);
        }
    }
}
