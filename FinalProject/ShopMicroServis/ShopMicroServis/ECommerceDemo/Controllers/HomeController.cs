using ECommerceDemo.Models;
using ECommerceDemo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ECommerceDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClientHandler clientHandler;
        HttpClient client;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Cart()
        {
            MainCartVM main = new();
            var responseMessage = await client.GetStringAsync("https://localhost:7014/items?userId=3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var cartVM = JsonConvert.DeserializeObject<List<CartVM>>(responseMessage);
            main.Carts = cartVM;
            return View(main);
        }

        [HttpPost]
        public async Task<IActionResult> Cart(MainCartVM mainCartVM)
        {
            AddCartVM addCart = mainCartVM.AddCart;
            var jsonaddCartVM = JsonConvert.SerializeObject(addCart);
            StringContent content = new StringContent(jsonaddCartVM, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"https://localhost:7014/items?userId={addCart.UserId}",content);
            return RedirectToAction("Cart");
        }


        public IActionResult Profile()
        {
            return View();
        }

        public async Task<IActionResult> Shop()
        {
            ViewBag.Success = TempData["success"];
            MainShopVM main = new();
            var responseMessage = await client.GetStringAsync("https://localhost:7098/items");
            var shopVM = JsonConvert.DeserializeObject<List<ShopVM>>(responseMessage);
            main.Shops = shopVM;
            return View(main);
        }
        [HttpPost]
        public async Task<IActionResult> Shop(MainShopVM mainShopVM)
        {

            AddCartVM addCart = mainShopVM.AddCart;
            var jsonaddCartVM = JsonConvert.SerializeObject(addCart);
            StringContent content = new StringContent(jsonaddCartVM, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"https://localhost:7014/items?userId={addCart.UserId}", content);
            TempData["success"] = "true";
            return RedirectToAction("Shop");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}