using AdminPanel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AdminPanel.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Index()
        {
            var responseMessage = await client.GetStringAsync("https://localhost:51612/api/Values");
            var valuesVM = JsonConvert.DeserializeObject<List<ValuesVM>>(responseMessage);
            return View(valuesVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}