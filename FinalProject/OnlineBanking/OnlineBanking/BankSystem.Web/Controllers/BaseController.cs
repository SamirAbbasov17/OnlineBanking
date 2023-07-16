using BankSystem.Common;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankSystem.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult RedirectToHome() => this.RedirectToAction("Index", "Home");

        protected string GetCurrentUserId()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var claim = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        protected void ShowErrorMessage(string message)
        {
            this.TempData[GlobalConstants.TempDataErrorMessageKey] = message;
        }

        protected void ShowSuccessMessage(string message)
        {
            this.TempData[GlobalConstants.TempDataSuccessMessageKey] = message;
        }
    }
}
