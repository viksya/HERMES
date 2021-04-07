using HermesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HermesLogic.Managers;
using HermesLogic.DB;

namespace HermesWeb.Controllers
{
    public class HomeController : Controller
    {
        private LoginManager loginManager = new LoginManager();

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
           
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {

            if (HttpContext.Session.isSignedIn())
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = loginManager.GetUser(model.Username, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("validation", "Incorrect username/password!");
                }
                else
                {
                   // HttpContext.Session.SetUsername(user.Username);

                    return RedirectToAction("Index", "Chat");
                }
            }
            return View(model);

        }
        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
