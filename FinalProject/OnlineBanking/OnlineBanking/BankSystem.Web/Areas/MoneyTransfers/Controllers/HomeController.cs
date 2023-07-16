using AutoMapper;
using BankSystem.Services.BankAccount;
using BankSystem.Services.BankMoneyTransfer;
using BankSystem.Services.Models.BankMoneyTransfer;
using BankSystem.Web.Areas.MoneyTransfers.Models;
using BankSystem.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Web.Areas.MoneyTransfers.Controllers
{
    public class HomeController : BaseMoneyTransferController
    {
        private const int PaymentsCountPerPage = 10;

        private readonly IMoneyTransferService moneyTransferService;

        public HomeController(
            IAccountService bankAccountService,
            IMoneyTransferService moneyTransferService,
            IMapper mapper)
            : base(bankAccountService, mapper)
        {
            this.moneyTransferService = moneyTransferService;
        }

        [Route("/{area}/Archives")]
        public async Task<IActionResult> All(int pageIndex = 1)
        {
            pageIndex = Math.Max(1, pageIndex);

            var userId = this.GetCurrentUserId();
            var allMoneyTransfers =
                (await this.moneyTransferService.GetMoneyTransfersAsync<MoneyTransferListingServiceModel>(userId,
                    pageIndex, PaymentsCountPerPage))
                .Select(this.Mapper.Map<MoneyTransferListingDto>)
                .ToPaginatedList(await this.moneyTransferService.GetCountOfAllMoneyTransfersForUserAsync(userId),
                    pageIndex, PaymentsCountPerPage);

            var transfers = new MoneyTransferListingViewModel
            {
                MoneyTransfers = allMoneyTransfers
            };

            return this.View(transfers);
        }
    }
}
