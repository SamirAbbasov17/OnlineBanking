using AutoMapper;
using BankSystem.Services.BankMoneyTransfer;
using BankSystem.Services.Models.BankMoneyTransfer;
using BankSystem.Web.Areas.Administration.Models;
using BankSystem.Web.Areas.MoneyTransfers.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Web.Areas.Administration.Controllers
{

    public class TransactionsController : BaseAdministrationController
    {
        private readonly IMoneyTransferService moneyTransfer;
        private readonly IMapper mapper;

        public TransactionsController(IMoneyTransferService moneyTransfer, IMapper mapper)
        {
            this.moneyTransfer = moneyTransfer;
            this.mapper = mapper;
        }

        public IActionResult Search() => this.View();

        public async Task<IActionResult> Result(string referenceNumber)
        {
            if (referenceNumber == null)
            {
                return this.NotFound();
            }

            var moneyTransfers = (await this.moneyTransfer
                    .GetMoneyTransferAsync<MoneyTransferListingServiceModel>(referenceNumber))
                .Select(this.mapper.Map<MoneyTransferListingDto>);

            var viewModel = new TransactionListingViewModel
            {
                MoneyTransfers = moneyTransfers,
                ReferenceNumber = referenceNumber
            };

            return this.View(viewModel);
        }
    }
}
