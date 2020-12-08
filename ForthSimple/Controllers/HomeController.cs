using ForthSimple.Interfaces;
using ForthSimple.Models;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger<HomeController> logger;
        private readonly ForthDbContext dbContext;
        private readonly ICookieIdentityService identityService;
        public HomeController(ILogger<HomeController> logger, ForthDbContext dbContext, ICookieIdentityService identityService)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.identityService = identityService;
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
            return View(new UserSignUpVM());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpVM signUpVM)
        {
            if (!ModelState.IsValid)
                return View(signUpVM);

            var res = await identityService.SignUpAsync(signUpVM, HttpContext);
            if (!res)
            {
                ModelState.AddModelError("Register error","User already exists");
                return View(signUpVM);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View(new UserSignInVM());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInVM signInVM)
        {
            if (!ModelState.IsValid)
                return View(signInVM);

            if (!await identityService.SignInAsync(signInVM, HttpContext))
            {
                ModelState.AddModelError("Authorize error", "Invalid login/password");
                return View(signInVM);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SignOut()
        {
            await identityService.LogoutAsync(HttpContext);
            
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult UsersTable()
        {
            return View();
        }
    }
}
