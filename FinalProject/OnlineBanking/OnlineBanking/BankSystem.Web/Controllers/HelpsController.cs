using BankSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BankSystem.Web.Controllers
{
    public class HelpsController : Controller
    {
        HttpClientHandler clientHandler;
        HttpClient client;
        public HelpsController()
        {
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
        }
        public async Task<IActionResult> Index()
        {

            var responseMessage = await client.GetStringAsync($"https://localhost:7178/api/MainApi/Help");
            var help = JsonConvert.DeserializeObject<List<HelpVM>>(responseMessage);
            return View(help);
        }
        [HttpGet]
        public async Task<IActionResult> HelpDetails(int id)
        {
            var responseMessage = await client.GetStringAsync($"https://localhost:7178/api/MainApi/HelpById/{id}");
            var help = JsonConvert.DeserializeObject<HelpVM>(responseMessage);
            return View(help);
        }
    }
}
