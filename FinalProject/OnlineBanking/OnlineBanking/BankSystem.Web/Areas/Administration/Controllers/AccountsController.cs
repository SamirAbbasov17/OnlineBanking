using AutoMapper;
using BankSystem.Services.BankAccount;
using BankSystem.Services.Models.BankAccount;
using BankSystem.Web.Areas.Administration.Models;
using BankSystem.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Web.Areas.Administration.Controllers
{
    public class AccountsController : BaseAdministrationController
    {
        private const int AccountsPerPage = 20;

        private readonly IAccountService bankAccountService;
        private readonly IMapper mapper;

        public AccountsController(IAccountService bankAccountService, IMapper mapper)
        {
            this.bankAccountService = bankAccountService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            pageIndex = Math.Max(1, pageIndex);

            var allAccounts = (await this.bankAccountService.GetAccountsAsync<AccountDetailsServiceModel>(pageIndex, AccountsPerPage))
                .Select(this.mapper.Map<AccountListingViewModel>)
                .ToPaginatedList(await this.bankAccountService.GetCountOfAccountsAsync(), pageIndex, AccountsPerPage);

            var transfers = new AllAccountsListViewModel
            {
                BankAccounts = allAccounts
            };

            return this.View(transfers);
        }
    }
}
