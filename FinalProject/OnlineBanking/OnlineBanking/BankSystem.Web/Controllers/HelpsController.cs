using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Web.Controllers
{
    public class HelpsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HelpDetails()
        {
            return View();
        }
    }
}
