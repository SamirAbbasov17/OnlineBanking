using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorsController : BaseController
    {
        [Route("error/404")]
        public IActionResult Error404()
            => this.View();

        [Route("error/403")]
        public IActionResult Error403()
            => this.View();

        [Route("error/{code:int}")]
        public IActionResult Error(int code)
            => this.View();
    }
}
