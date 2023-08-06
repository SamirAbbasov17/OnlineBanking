using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Web.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
