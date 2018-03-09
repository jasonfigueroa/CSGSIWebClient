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
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMatchRepository _matchRepository;
        private User _user;

        public HomeController(IUserService userService, IMatchRepository matchRepository)
        {
            _userService = userService;
            _user = userService.GetUser();
            _matchRepository = matchRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            if (_userService.GetLogIn().LoggedIn == false)
            {
                return RedirectToAction("Index", "Login");
            }

            var matches = _matchRepository.GetAllMatches();

            var homeViewModel = new HomeViewModel()
            {
                Title = "Match Data",
                Matches = matches.ToList()
            };

            return View(homeViewModel);
        }

        public IActionResult Details(int id)
        {
            if (_userService.GetLogIn().LoggedIn == false)
            {
                return RedirectToAction("Index", "Login");
            }

            //var match = _matchRepository.GetMatchById(id);
            var match = APIInterface.GetCSMatch(_user, id);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }
    }
}
