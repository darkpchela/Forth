using ForthSimple.Interfaces;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ForthSimple.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICookieIdentityService identityService;

        public HomeController(ICookieIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public IActionResult Index()
        {
            return View();
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
                ModelState.AddModelError("Register error", "User already exists");
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
    }
}