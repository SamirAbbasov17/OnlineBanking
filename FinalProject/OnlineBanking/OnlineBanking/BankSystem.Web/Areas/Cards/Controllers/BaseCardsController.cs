using BankSystem.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Web.Areas.Cards.Controllers
{
    [Area("Cards")]
    [Route("[area]/[action]/{id?}")]
    public abstract class BaseCardsController : BaseController
    {
    }
}
