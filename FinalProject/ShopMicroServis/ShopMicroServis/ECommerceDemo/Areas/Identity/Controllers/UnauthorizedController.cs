﻿using ECommerceDemo.Areas.Identity.ViewModels;
using ECommerceDemo.Data;
using ECommerceDemo.Models;
using ECommerceDemo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ECommerceDemo.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class UnauthorizedController : Controller
    {
        HttpClientHandler clientHandler;
        HttpClient client;
        private readonly UserManager<ECommerceDemoUser> _userManager;
        private readonly SignInManager<ECommerceDemoUser> _signInManager;
        private readonly ECommerceDataDbContext eCommerceDataDbContext;

        public UnauthorizedController(UserManager<ECommerceDemoUser> userManager, SignInManager<ECommerceDemoUser> signInManager)
        {
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            //if (eCommerceDataDbContext?.Users == null)
            //{
            //    var user = new ECommerceDemoUser
            //    {
            //        Email = "test@test.com",
            //        UserName = "test@test.com",
            //        EmailConfirmed = true,
            //    };

            //    await _userManager.CreateAsync(user, "Test123$");
            //}


            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { Area = "" });
            return View();
        }
        public async Task<IActionResult> Shop()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home" , new { Area = "" });
            MainShopVM main = new();
            var responseMessage = await client.GetStringAsync("https://localhost:7098/items");
            var shopVM = JsonConvert.DeserializeObject<List<ShopVM>>(responseMessage);
            main.Shops = shopVM;
            return View(main);
        }
        public async Task<IActionResult> Identity()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { Area = "" });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVm)
        {
            ECommerceDemoUser user;
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
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVm)
        {
            if (!ModelState.IsValid) return View();

            ECommerceDemoUser newUser = new ECommerceDemoUser()
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
            return RedirectToAction("Index", "Unauthorized", new { Area = "Identity" });
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Unauthorized", new { Area = "Identity" });
        }
    }
}
