using BankSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BankSystem.Web.Controllers
{
    public class ContactsController : Controller
    {
        HttpClientHandler clientHandler;
        HttpClient client;

        public ContactsController()
        {
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Success1 = TempData["success1"];
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Send(ContactMessageVM contactMessageVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var contact = JsonConvert.SerializeObject(contactMessageVM);
            StringContent content = new StringContent(contact, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"https://localhost:7178/api/MainApi/Contact", content);
            TempData["success1"] = "true1";
            return RedirectToAction("Index");
        }
    }
}
