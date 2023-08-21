using AutoMapper;
using BankSystem.Services.BankAccount;
using BankSystem.Services.Models.BankAccount;
using BankSystem.Web.Areas.Administration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAccountService bankAccountService;
        private readonly IMapper mapper;

        public ValuesController(IAccountService bankAccountService, IMapper mapper)
        {
            this.bankAccountService = bankAccountService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allAccounts = (await this.bankAccountService.GetAccountsAsync<AccountDetailsServiceModel>())
                .Select(this.mapper.Map<AccountListingViewModel>).ToList();

            return allAccounts != null ?
                         Ok(allAccounts) :
                         Problem("Entity is null.");
        }
    }
}
