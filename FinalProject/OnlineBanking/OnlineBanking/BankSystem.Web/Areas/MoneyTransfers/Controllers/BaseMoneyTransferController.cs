using AutoMapper;
using BankSystem.Services.BankAccount;
using BankSystem.Services.Models.BankAccount;
using BankSystem.Web.Controllers;
using BankSystem.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Web.Areas.MoneyTransfers.Controllers
{
    [Area("MoneyTransfers")]
    public abstract class BaseMoneyTransferController : BaseController
    {
        private readonly IAccountService bankAccountService;

        protected BaseMoneyTransferController(IAccountService bankAccountService, IMapper mapper)
        {
            this.bankAccountService = bankAccountService;
            this.Mapper = mapper;
        }

        protected IMapper Mapper { get; }

        protected async Task<OwnAccountListingViewModel[]> GetAllAccountsAsync(string userId)
            => (await this.bankAccountService
                    .GetAllAccountsByUserIdAsync<AccountIndexServiceModel>(userId))
                .Select(this.Mapper.Map<OwnAccountListingViewModel>)
                .ToArray();
    }
}
