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
using HermesLogic;
using System.Net.Mail;


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
                    HttpContext.Session.SetUsername(user.Username);

                    return RedirectToAction("Index", "Chat");
                }
            }
            return View("Index");
            //return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.isSignedIn())
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    loginManager.Register(model.Username, model.Email, model.Password);

                    //return RedirectToAction("Login");
                    return RedirectToAction("Index", "Home");
                }
                catch (LogicException exception)
                {
                    ModelState.AddModelError("validation", exception.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            if (HttpContext.Session.isSignedIn())
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ResetModel model)
        {
            if (ModelState.IsValid)
            {
                var user = loginManager.GetUserByEmail(model.Username, model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("validation", "Email not found!");
                }

                var password = loginManager.PasswordGenerator();
                loginManager.updatePassword(user, password);

                
                MailMessage mailObj = new MailMessage("sgthermes@inbox.lv", user.Email, "Hermes password reset", "your new password is: " + password);
                var client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("sgthermes", "T4T?ZMy2wb");
                client.Host = "mail.inbox.lv";
                client.EnableSsl = true;
                client.Port = 587;
                client.Send(mailObj);






                //  var callbackUrl = Url.Action("ResetPassword", "Account",
                //new { UserId = user.Id, code = password }, protocol: Request.Url.Scheme);
                //    UserManager.SendEmailAsync(user.Id, "Reset Password",
                //"Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");



                return View(model);
            }


            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }








    }
}