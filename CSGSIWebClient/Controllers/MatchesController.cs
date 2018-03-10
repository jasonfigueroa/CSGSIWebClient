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
    public class MatchesController : Controller
    {
        private readonly IUserService _userService;
        private User _user;

        public MatchesController(IUserService userService)
        {
            _userService = userService;
            _user = userService.GetUser();
        }

        public IActionResult Index()
        {
            if (_userService.GetLogIn().LoggedIn == false)
            {
                return RedirectToAction("Index", "Login");
            }

            MatchesViewModel matchesViewModel = APIInterface.GetCSMatches(_user);

            matchesViewModel.Title = "Matches";

            return View(matchesViewModel);
        }

        public IActionResult Match(int id)
        {
            if (_userService.GetLogIn().LoggedIn == false)
            {
                return RedirectToAction("Index", "Login");
            }

            CSMatch match = APIInterface.GetCSMatch(_user, id);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }
    }
}
