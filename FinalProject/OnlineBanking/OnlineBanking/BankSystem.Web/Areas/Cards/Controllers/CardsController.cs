﻿using AutoMapper;
using BankSystem.Common;
using BankSystem.Services.BankAccount;
using BankSystem.Services.BankCard;
using BankSystem.Services.Models.BankAccount;
using BankSystem.Services.Models.BankCard;
using BankSystem.Web.Areas.Cards.Models;
using BankSystem.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace BankSystem.Web.Areas.Cards.Controllers
{
    public class CardsController : BaseCardsController
    {
        private const int CardsCountPerPage = 10;
        private readonly IAccountService bankAccountService;
        private readonly ICardService cardService;
        private readonly IMapper mapper;

        public CardsController(
            IAccountService bankAccountService,
            ICardService cardService,
            IMapper mapper)
        {
            this.bankAccountService = bankAccountService;
            this.cardService = cardService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            pageIndex = Math.Max(1, pageIndex);

            var userId = this.GetCurrentUserId();
            var allCards = (await this.cardService
                    .GetCardsAsync<CardDetailsServiceModel>(userId, pageIndex, CardsCountPerPage))
                .Select(this.mapper.Map<CardListingDto>)
                .ToPaginatedList(await this.cardService.GetCountOfAllCardsOwnedByUserAsync(userId), pageIndex,
                    CardsCountPerPage);

            var cards = new CardListingViewModel
            {
                Cards = allCards
            };

            return this.View(cards);
        }

        public async Task<IActionResult> Create()
        {
            var userAccounts = await this.GetAllAccountsAsync(this.GetCurrentUserId());

            var model = new CardCreateViewModel
            {
                BankAccounts = userAccounts
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CardCreateViewModel model)
        {
            var userId = this.GetCurrentUserId();
            if (!this.ModelState.IsValid)
            {
                model.BankAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            var account = await this.bankAccountService.GetByIdAsync<AccountDetailsServiceModel>(model.AccountId);
            if (account == null || account.UserId != userId)
            {
                return this.Forbid();
            }

            var serviceModel = this.mapper.Map<CardCreateServiceModel>(model);
            serviceModel.UserId = userId;
            serviceModel.Name = account.UserFullName;
            serviceModel.ExpiryDate = DateTime.UtcNow.AddYears(GlobalConstants.CardValidityInYears)
                .ToString(GlobalConstants.CardExpirationDateFormat, CultureInfo.InvariantCulture);

            bool isCreated = await this.cardService.CreateAsync(serviceModel);
            if (!isCreated)
            {
                this.ShowErrorMessage(NotificationMessages.CardCreateError);

                return this.RedirectToHome();
            }

            this.ShowSuccessMessage(NotificationMessages.CardCreatedSuccessfully);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                this.ShowErrorMessage(NotificationMessages.CardDoesNotExist);

                return this.RedirectToAction(nameof(this.Index));
            }

            var card = await this.cardService.GetAsync<CardDetailsServiceModel>(id);
            if (card == null || card.UserId != this.GetCurrentUserId())
            {
                this.ShowErrorMessage(NotificationMessages.CardDoesNotExist);

                return this.RedirectToAction(nameof(this.Index));
            }

            var isDeleted = await this.cardService.DeleteAsync(id);
            if (!isDeleted)
            {
                this.ShowErrorMessage(NotificationMessages.CardDeleteError);
            }
            else
            {
                this.ShowSuccessMessage(NotificationMessages.CardDeletedSuccessfully);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task<IEnumerable<SelectListItem>> GetAllAccountsAsync(string userId)
            => (await this.bankAccountService
                    .GetAllAccountsByUserIdAsync<AccountIndexServiceModel>(userId))
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id })
                .ToArray();
    }
}
