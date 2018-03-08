using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSGSIWebClient.Models;
using CSGSIWebClient.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSGSIWebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMatchRepository _matchRepository;

        public HomeController(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

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

            var match = _matchRepository.GetMatchById(id);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }
    }
}
