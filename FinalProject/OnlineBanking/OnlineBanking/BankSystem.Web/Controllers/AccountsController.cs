using AutoMapper;
using BankSystem.Common.Configuration;
using BankSystem.Common;
using BankSystem.Services.BankMoneyTransfer;
using BankSystem.Services.Models.BankMoneyTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using BankSystem.Services.BankAccount;
using BankSystem.Services.Models.BankAccount;
using BankSystem.Web.Models.Account;
using BankSystem.Web.Infrastructure.Extensions;
using BankSystem.Web.Areas.MoneyTransfers.Models;

namespace BankSystem.Web.Controllers
{
    public class AccountsController : BaseController
    {
        private const int ItemsPerPage = 10;

        private readonly IAccountService bankAccountService;
        private readonly BankConfiguration bankConfiguration;
        private readonly IMoneyTransferService moneyTransferService;
        private readonly IMapper mapper;

        public AccountsController(
            IAccountService bankAccountService,
            IMoneyTransferService moneyTransferService,
            IOptions<BankConfiguration> bankConfigurationOptions,
            IMapper mapper)
        {
            this.bankAccountService = bankAccountService;
            this.moneyTransferService = moneyTransferService;
            this.mapper = mapper;
            this.bankConfiguration = bankConfigurationOptions.Value;
        }

        public IActionResult Create()
            => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(AccountCreateBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var serviceModel = this.mapper.Map<AccountCreateServiceModel>(model);
            serviceModel.UserId = this.GetCurrentUserId();

            var accountId = await this.bankAccountService.CreateAsync(serviceModel);
            if (accountId == null)
            {
                this.ShowErrorMessage(NotificationMessages.BankAccountCreateError);

                return this.View(model);
            }

            this.ShowSuccessMessage(NotificationMessages.BankAccountCreated);

            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Details(string id, int pageIndex = 1)
        {
            pageIndex = Math.Max(1, pageIndex);

            var account = await this.bankAccountService.GetByIdAsync<AccountDetailsServiceModel>(id);
            if (account == null ||
                account.UserId != this.GetCurrentUserId())
            {
                return this.Forbid();
            }

            var allTransfersCount = await this.moneyTransferService.GetCountOfAllMoneyTransfersForAccountAsync(id);
            var transfers = (await this.moneyTransferService
                    .GetMoneyTransfersForAccountAsync<MoneyTransferListingServiceModel>(id, pageIndex, ItemsPerPage))
                .Select(this.mapper.Map<MoneyTransferListingDto>)
                .ToPaginatedList(allTransfersCount, pageIndex, ItemsPerPage);

            var viewModel = this.mapper.Map<AccountDetailsViewModel>(account);
            viewModel.MoneyTransfers = transfers;
            viewModel.MoneyTransfersCount = allTransfersCount;
            viewModel.BankName = this.bankConfiguration.BankName;
            viewModel.BankCode = this.bankConfiguration.UniqueIdentifier;
            viewModel.BankCountry = this.bankConfiguration.Country;

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAccountNameAsync(string accountId, string name)
        {
            if (name == null)
            {
                return this.Ok(new
                {
                    success = false
                });
            }

            var account = await this.bankAccountService.GetByIdAsync<AccountDetailsServiceModel>(accountId);
            if (account == null ||
                account.UserId != this.GetCurrentUserId())
            {
                return this.Ok(new
                {
                    success = false
                });
            }

            bool isSuccessful = await this.bankAccountService.ChangeAccountNameAsync(accountId, name);

            return this.Ok(new
            {
                success = isSuccessful
            });
        }
    }
}
