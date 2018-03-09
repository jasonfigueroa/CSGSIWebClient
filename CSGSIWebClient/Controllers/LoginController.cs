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

            //if (_userService.GetUser() == null)
            //{
            //    _user = new User();
            //}

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
                if(APIInterface.IsValidUser(user))
                {
                    _userService.SetUser(user);

                    // testing
                    //Login login = new Login();
                    //login.LoggedIn = true;

                    _userService.SetLogIn(new Login { LoggedIn = true });
                    // end test

                    return RedirectToAction("SuccessfulLogin");
                }
            }

            return View(user);
        }

        //static async Task<JWT> Auth(User user)
        //{
        //    string url = "http://localhost:5000/auth";

        //    using (HttpClient client = new HttpClient())

        //    using (HttpResponseMessage res = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")))
        //    using (HttpContent content = res.Content)
        //    {
        //        string data = await content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<JWT>(data);
        //    }
        //}

        public IActionResult SuccessfulLogin()
        {
            // to control user routing
            if (_userService.GetLogIn().LoggedIn == false)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}
