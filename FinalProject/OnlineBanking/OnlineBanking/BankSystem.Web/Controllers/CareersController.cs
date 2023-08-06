using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Web.Controllers
{
    public class CareersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Position()
        {
            return View();
        }
    }
}
