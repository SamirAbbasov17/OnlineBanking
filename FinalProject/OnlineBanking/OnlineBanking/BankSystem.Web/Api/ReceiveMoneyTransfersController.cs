using BankSystem.Services.BankAccount;
using BankSystem.Services.BankMoneyTransfer;
using BankSystem.Services.Models.BankAccount;
using BankSystem.Services.Models.BankMoneyTransfer;
using BankSystem.Web.Api.Models;
using BankSystem.Web.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BankSystem.Web.Api
{

    [ApiController]
    [Route("api/[controller]")]
    [DecryptAndVerifyRequest]
    [IgnoreAntiforgeryToken]
    public class ReceiveMoneyTransfersController : ControllerBase
    {
        private readonly IAccountService bankAccountService;
        private readonly IMoneyTransferService moneyTransferService;

        public ReceiveMoneyTransfersController(
            IMoneyTransferService moneyTransferService,
            IAccountService bankAccountService)
        {
            this.moneyTransferService = moneyTransferService;
            this.bankAccountService = bankAccountService;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string data)
        {
            var model = JsonConvert.DeserializeObject<ReceiveMoneyTransferModel>(data);
            if (!this.TryValidateModel(model))
            {
                return this.BadRequest();
            }

            var account =
                await this.bankAccountService.GetByUniqueIdAsync<AccountConciseServiceModel>(
                    model.DestinationBankAccountUniqueId);
            if (account == null || !string.Equals(account.UserFullName, model.RecipientName,
                StringComparison.InvariantCulture))
            {
                return this.BadRequest();
            }

            var serviceModel = new MoneyTransferCreateServiceModel
            {
                AccountId = account.Id,
                Amount = model.Amount,
                Description = model.Description,
                DestinationAccountUniqueId = model.DestinationBankAccountUniqueId,
                Source = model.SenderAccountUniqueId,
                SenderName = model.SenderName,
                RecipientName = model.RecipientName,
                ReferenceNumber = model.ReferenceNumber
            };

            var isSuccessful = await this.moneyTransferService.CreateMoneyTransferAsync(serviceModel);
            if (!isSuccessful)
            {
                return this.NoContent();
            }

            return this.Ok();
        }
    }
}
