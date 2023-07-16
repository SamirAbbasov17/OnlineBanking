using AutoMapper;
using BankSystem.Common;
using BankSystem.Services.BankAccount;
using BankSystem.Services.BankMoneyTransfer;
using BankSystem.Services.Models.BankAccount;
using BankSystem.Services.Models.BankMoneyTransfer;
using BankSystem.Web.Areas.MoneyTransfers.Models.Internal;
using BankSystem.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Web.Areas.MoneyTransfers.Controllers
{
    public class InternalController : BaseMoneyTransferController
    {
        private readonly IAccountService bankAccountService;
        private readonly IMoneyTransferService moneyTransferService;

        public InternalController(
            IMoneyTransferService moneyTransferService,
            IAccountService bankAccountService,
            IMapper mapper)
            : base(bankAccountService, mapper)
        {
            this.moneyTransferService = moneyTransferService;
            this.bankAccountService = bankAccountService;
        }

        public async Task<IActionResult> Create()
        {
            var userId = this.GetCurrentUserId();
            var userAccounts = await this.GetAllAccountsAsync(userId);

            if (!userAccounts.Any())
            {
                this.ShowErrorMessage(NotificationMessages.NoAccountsError);

                return this.RedirectToHome();
            }

            var model = new InternalMoneyTransferCreateBindingModel
            {
                OwnAccounts = userAccounts
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InternalMoneyTransferCreateBindingModel model)
        {
            var userId = this.GetCurrentUserId();

            if (!this.ModelState.IsValid)
            {
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            var account = await this.bankAccountService.GetByIdAsync<AccountDetailsServiceModel>(model.AccountId);
            if (account == null || account.UserId != userId)
            {
                return this.Forbid();
            }

            if (string.Equals(account.UniqueId, model.DestinationAccountUniqueId,
                StringComparison.InvariantCulture))
            {
                this.ShowErrorMessage(NotificationMessages.SameAccountsError);
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            if (account.Balance < model.Amount)
            {
                this.ShowErrorMessage(NotificationMessages.InsufficientFunds);
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            var destinationAccount =
                await this.bankAccountService.GetByUniqueIdAsync<AccountConciseServiceModel>(
                    model.DestinationAccountUniqueId);
            if (destinationAccount == null)
            {
                this.ShowErrorMessage(NotificationMessages.DestinationBankAccountDoesNotExist);
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            var referenceNumber = ReferenceNumberGenerator.GenerateReferenceNumber();
            var sourceServiceModel = this.Mapper.Map<MoneyTransferCreateServiceModel>(model);
            sourceServiceModel.Source = account.UniqueId;
            sourceServiceModel.Amount *= -1;
            sourceServiceModel.SenderName = account.UserFullName;
            sourceServiceModel.RecipientName = destinationAccount.UserFullName;
            sourceServiceModel.ReferenceNumber = referenceNumber;

            if (!await this.moneyTransferService.CreateMoneyTransferAsync(sourceServiceModel))
            {
                this.ShowErrorMessage(NotificationMessages.TryAgainLaterError);
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            var destinationServiceModel = this.Mapper.Map<MoneyTransferCreateServiceModel>(model);
            destinationServiceModel.Source = account.UniqueId;
            destinationServiceModel.AccountId = destinationAccount.Id;
            destinationServiceModel.SenderName = account.UserFullName;
            destinationServiceModel.RecipientName = destinationAccount.UserFullName;
            destinationServiceModel.ReferenceNumber = referenceNumber;

            if (!await this.moneyTransferService.CreateMoneyTransferAsync(destinationServiceModel))
            {
                this.ShowErrorMessage(NotificationMessages.TryAgainLaterError);
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            this.ShowSuccessMessage(NotificationMessages.SuccessfulMoneyTransfer);

            return this.RedirectToHome();
        }
    }
}
