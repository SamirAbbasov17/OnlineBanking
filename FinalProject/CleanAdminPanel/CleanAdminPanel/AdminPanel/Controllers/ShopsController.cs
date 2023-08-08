using AdminPanel.ViewModels;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace AdminPanel.Controllers
{

    [Authorize]
    public class ShopsController : Controller
    {
        HttpClientHandler clientHandler;
        HttpClient client;

        public ShopsController()
        {
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
        }
        public async Task<IActionResult> Index()
        {
            var responseMessage = await client.GetStringAsync("https://localhost:7098/items");
            var shopVM = JsonConvert.DeserializeObject<List<ShopVM>>(responseMessage);
            return View(shopVM);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ShopCreateVM shopVM)
        {
            shopVM.Image = "d/img";
            var add = JsonConvert.SerializeObject(shopVM);
            StringContent content = new StringContent(add, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7098/items", content);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(string id)
        {
            var responseMessage = await client.GetStringAsync($"https://localhost:7098/items/{id}");
            var shopVM = JsonConvert.DeserializeObject<ShopVM>(responseMessage);
            return View(shopVM);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var responseMessage = await client.GetStringAsync($"https://localhost:7098/items/{id}");
            var shopVM = JsonConvert.DeserializeObject<ShopVM>(responseMessage);
            return View(shopVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ShopVM shopVM)
        {
            var responseMessage = await client.DeleteAsync($"https://localhost:7098/items/{shopVM.Id}");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(string id)
        {

            var responseMessage = await client.GetStringAsync($"https://localhost:7098/items/{id}");
            var shopVM = JsonConvert.DeserializeObject<ShopVM>(responseMessage);
            return View(shopVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ShopVM shopVM)
        {
            shopVM.Image = "d/img";
            var add = JsonConvert.SerializeObject(shopVM);
            StringContent content = new StringContent(add, Encoding.UTF8, "application/json"); 
            var responseMessage = await client.PutAsync($"https://localhost:7098/items/{shopVM.Id}", content);  
            return RedirectToAction("Index");
        }
    }
}
