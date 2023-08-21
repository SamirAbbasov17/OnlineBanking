using AdminPanel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
		public async Task<IActionResult> Chart()
		{
			var responseMessage = await client.GetStringAsync("https://localhost:51612/api/Values");
			var valuesVM = JsonConvert.DeserializeObject<List<ValuesVM>>(responseMessage);
            var balance = valuesVM
			.Select(x => x.Balance).ToArray();
			var users = valuesVM
		   .Select(x => x.UserUserName).Distinct().ToArray();
			var accounts = valuesVM
		   .Select(x => x.Name).ToArray();

			return new JsonResult(new { userBalance = balance, userUsers = users, userAccount = accounts  });
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