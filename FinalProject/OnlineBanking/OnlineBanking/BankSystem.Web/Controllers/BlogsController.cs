using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Web.Controllers
{
    public class BlogsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SinglePage()
        {
            return View();
        }
    }
}
