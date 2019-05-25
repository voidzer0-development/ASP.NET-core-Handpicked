using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B2Handpicked.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace B2Handpicked.UI.Controllers {
    public class AccountController : Controller {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel authModel) {
            if (ModelState.IsValid && await SignIn(authModel.Username, authModel.Password)) {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email en/of wachtwoord zijn verkeerd");
            return View();
        }

        public async Task<IActionResult> Logout() {
            await SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel authModel) {
            if (ModelState.IsValid) {
                var registrationResult = await Register(authModel.Username, authModel.Email, authModel.PhoneNumber, authModel.Password);
                if (registrationResult.Item1) return RedirectToAction("Index", "Home");
                else foreach (string error in registrationResult.Item2) {
                    ModelState.AddModelError("", error);
                }
            }

            return View();
        }

        private async Task SignOut() {
            await _signInManager.SignOutAsync();
        }

        private async Task<bool> SignIn(string username, string password) {
            IdentityUser user = await _userManager.FindByNameAsync(username);
            if (user != null) {
                await SignOut();
                return (await _signInManager.PasswordSignInAsync(user, password, false, false)).Succeeded;
            }
            return false;
        }

        private async Task<(bool, IEnumerable<string>)> Register(string username, string email, string phone, string password) {
            var user = new IdentityUser {
                UserName = username, 
                Email = email,
                PhoneNumber = phone
            };
            var registrationResult = await _userManager.CreateAsync(user, password);
            if (registrationResult.Succeeded) {
                await SignIn(email, password);
                return (true, new List<string>());
            }
            return (false, registrationResult.Errors.Select(item => item.Description));
        }
    }
}