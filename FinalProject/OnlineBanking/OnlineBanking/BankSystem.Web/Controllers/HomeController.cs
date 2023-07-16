using AutoMapper;
using BankSystem.Services.BankAccount;
using BankSystem.Services.BankMoneyTransfer;
using BankSystem.Services.Models.BankAccount;
using BankSystem.Services.Models.BankMoneyTransfer;
using BankSystem.Web.Areas.MoneyTransfers.Models;
using BankSystem.Web.Models;
using BankSystem.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankSystem.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IAccountService bankAccountService;
        private readonly IMoneyTransferService moneyTransferService;
        private readonly IMapper mapper;

        public HomeController(
            IAccountService bankAccountService,
            IMoneyTransferService moneyTransferService,
            IMapper mapper)
        {
            this.bankAccountService = bankAccountService;
            this.moneyTransferService = moneyTransferService;
            this.mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.View("IndexGuest");
            }

            var userId = this.GetCurrentUserId();

            var bankAccounts =
                (await this.bankAccountService.GetAllAccountsByUserIdAsync<AccountIndexServiceModel>(userId))
                .Select(this.mapper.Map<AccountIndexViewModel>)
                .ToArray();
            var moneyTransfers = (await this.moneyTransferService
                    .GetLast10MoneyTransfersForUserAsync<MoneyTransferListingServiceModel>(userId))
                .Select(this.mapper.Map<MoneyTransferListingDto>)
                .ToArray();

            var viewModel = new HomeViewModel
            {
                UserBankAccounts = bankAccounts,
                MoneyTransfers = moneyTransfers
            };

            return this.View(viewModel);
        }
    }
}