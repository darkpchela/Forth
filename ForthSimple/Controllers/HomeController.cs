using ForthSimple.Interfaces;
using ForthSimple.Models;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ForthSimple.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly UserVM _testUserVM = new UserVM { Id = 1, Email = "test@gmail.com", Password = "qwerty" };
        private readonly List<UserVM> _testList = new List<UserVM> { _testUserVM };
        private readonly ForthDbContext db;
        private readonly IUserIdentityService _identityService;
        public HomeController(ILogger<HomeController> logger, ForthDbContext dbContext, IUserIdentityService identityService)
        {
            db = dbContext;
            _logger = logger;
            _identityService = identityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserSignUpVM signUpVM)
        {
            if (_testList.Any(u => u.Email == signUpVM.Email))
                ModelState.AddModelError("Email", "Email address is already registered");

            if (!ModelState.IsValid)
                return View(signUpVM);
            
            return Json("Signed Up");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View(new UserSignInVM());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInVM signInVM)
        {
            if (ModelState.IsValid && await _identityService.SignInAsync(signInVM))
                return Json(true);

            return View(signInVM);
        }

        public IActionResult SignOut()
        {
            return RedirectToAction("SignIn");
        }

        public IActionResult UsersTable()
        {
            return View();
        }
    }
}
