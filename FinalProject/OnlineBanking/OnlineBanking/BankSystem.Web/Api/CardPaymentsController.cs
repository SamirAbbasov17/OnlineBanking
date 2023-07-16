﻿using AutoMapper;
using BankSystem.Common;
using BankSystem.Services.BankCard;
using BankSystem.Services.Models.BankCard;
using BankSystem.Web.Api.Models;
using BankSystem.Web.Infrastructure.Filters;
using BankSystem.Web.Infrastructure.Helpers.GlobalTransferHelpers.Models;
using BankSystem.Web.Infrastructure.Helpers.GlobalTransferHelpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace BankSystem.Web.Api
{
    [Route("api/[controller]")]
    [IgnoreAntiforgeryToken]
    [DecryptAndVerifyRequest]
    [ApiController]
    public class CardPaymentsController : ControllerBase
    {
        private readonly ICardService cardService;
        private readonly IGlobalTransferHelper globalTransferHelper;
        private readonly IMapper mapper;

        public CardPaymentsController(IGlobalTransferHelper globalTransferHelper, ICardService cardService, IMapper mapper)
        {
            this.globalTransferHelper = globalTransferHelper;
            this.cardService = cardService;
            this.mapper = mapper;
        }

        // POST: api/CardPayments
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string data)
        {
            var model = JsonConvert.DeserializeObject<PaymentInfoModel>(data);
            if (this.TryValidateModel(model))
            {
                return this.BadRequest();
            }

            var card = await this.cardService.GetAsync<CardDetailsServiceModel>(
                model.Number,
                model.ExpiryDate,
                model.SecurityCode,
                model.Name);

            if (card == null)
            {
                return this.BadRequest();
            }

            bool expirationDateValid = DateTime.TryParseExact(
                card.ExpiryDate,
                GlobalConstants.CardExpirationDateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var expirationDate);

            if (!expirationDateValid || expirationDate.AddMonths(1) < DateTime.UtcNow)
            {
                return this.BadRequest();
            }

            var serviceModel = this.mapper.Map<GlobalTransferDto>(model);
            serviceModel.SourceAccountId = card.AccountId;

            var result = await this.globalTransferHelper.TransferMoneyAsync(serviceModel);

            if (result != GlobalTransferResult.Succeeded)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}
