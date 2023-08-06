using AdminPanel.ViewModels;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVm)
        {
            ApplicationUser user;
            if (!ModelState.IsValid) return View();
            if (loginVm.UsernameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(loginVm.UsernameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                    return View();
                }
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginVm.UsernameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                    return View();
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVm)
        {
            if (!ModelState.IsValid) return View();

            ApplicationUser newUser = new ApplicationUser()
            {
                UserName = registerVm.Username,
                Email = registerVm.Email
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            return RedirectToAction("Login", "Auth");
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

    }
}
