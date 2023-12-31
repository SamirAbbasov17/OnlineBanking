﻿using AutoMapper;
using BankSystem.Common.Configuration;
using BankSystem.Common;
using BankSystem.Web.Infrastructure.Helpers.GlobalTransferHelpers.Models;
using BankSystem.Web.Infrastructure.Helpers.GlobalTransferHelpers;
using BankSystem.Web.Infrastructure.Helpers;
using BankSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Web;
using BankSystem.Services.BankAccount;
using BankSystem.Services.Models.BankAccount;
using BankSystem.Web.Models.Account;

namespace BankSystem.Web.Controllers
{
   
        [Authorize]
        public class PaymentsController : BaseController
        {
            private const int CookieValidityInMinutes = 5;
            private const string PaymentDataCookie = "PaymentData";
            private readonly IAccountService bankAccountService;

            private readonly BankConfiguration bankConfiguration;
            private readonly IGlobalTransferHelper globalTransferHelper;
            private readonly IMapper mapper;

            public PaymentsController(
                IOptions<BankConfiguration> bankConfigurationOptions,
                IAccountService bankAccountService,
                IGlobalTransferHelper globalTransferHelper,
                IMapper mapper)
            {
                this.bankConfiguration = bankConfigurationOptions.Value;
                this.bankAccountService = bankAccountService;
                this.globalTransferHelper = globalTransferHelper;
                this.mapper = mapper;
            }

            [HttpPost]
            [AllowAnonymous]
            [IgnoreAntiforgeryToken]
            [Route("/pay")]
            public IActionResult SetCookie(string data)
            {
                string decodedData;

                try
                {
                    decodedData = DirectPaymentsHelper.DecodePaymentRequest(data);
                }
                catch
                {
                    return this.BadRequest();
                }

                // set payment data cookie
                this.Response.Cookies.Append(PaymentDataCookie, decodedData,
                    new CookieOptions
                    {
                        SameSite = SameSiteMode.Lax,
                        IsEssential = true,
                        MaxAge = TimeSpan.FromMinutes(CookieValidityInMinutes)
                    });

                return this.RedirectToAction("Process");
            }

            [HttpGet]
            [Route("/pay")]
            public async Task<IActionResult> Process()
            {
                bool cookieExists = this.Request.Cookies.TryGetValue(PaymentDataCookie, out var data);

                if (!cookieExists)
                {
                    return this.RedirectToHome();
                }

                try
                {
                    dynamic paymentRequest =
                        DirectPaymentsHelper.ParsePaymentRequest(data, this.bankConfiguration.CentralApiPublicKey);
                    if (paymentRequest == null)
                    {
                        return this.BadRequest();
                    }

                    dynamic paymentInfo = DirectPaymentsHelper.GetPaymentInfo(paymentRequest);

                    var model = new PaymentConfirmBindingModel
                    {
                        Amount = paymentInfo.Amount,
                        Description = paymentInfo.Description,
                        DestinationBankName = paymentInfo.DestinationBankName,
                        DestinationBankCountry = paymentInfo.DestinationBankCountry,
                        DestinationBankAccountUniqueId = paymentInfo.DestinationBankAccountUniqueId,
                        RecipientName = paymentInfo.RecipientName,
                        OwnAccounts = await this.GetAllAccountsAsync(this.GetCurrentUserId()),
                        DataHash = DirectPaymentsHelper.Sha256Hash(data)
                    };

                    return this.View(model);
                }
                catch
                {
                    return this.BadRequest();
                }
            }

            [HttpPost]
            [IgnoreAntiforgeryToken]
            [Route("/Payments/PayAsync")]
            public async Task<IActionResult> PayAsync(PaymentConfirmBindingModel model)
            {
                bool cookieExists = this.Request.Cookies.TryGetValue(PaymentDataCookie, out var data);

                if (!this.ModelState.IsValid ||
                    !cookieExists ||
                    model.DataHash != DirectPaymentsHelper.Sha256Hash(data))
                {
                    return this.PaymentFailed(NotificationMessages.PaymentStateInvalid);
                }

                var account =
                    await this.bankAccountService.GetByIdAsync<AccountDetailsServiceModel>(model.AccountId);
                if (account == null || account.UserId != this.GetCurrentUserId())
                {
                    return this.Forbid();
                }

                try
                {
                    // read and validate payment data
                    dynamic paymentRequest =
                        DirectPaymentsHelper.ParsePaymentRequest(data, this.bankConfiguration.CentralApiPublicKey);

                    if (paymentRequest == null)
                    {
                        return this.PaymentFailed(NotificationMessages.PaymentStateInvalid);
                    }

                    dynamic paymentInfo = DirectPaymentsHelper.GetPaymentInfo(paymentRequest);

                    string returnUrl = paymentRequest.ReturnUrl;

                    // transfer money to destination account
                    var serviceModel = new GlobalTransferDto
                    {
                        Amount = paymentInfo.Amount,
                        Description = paymentInfo.Description,
                        DestinationBankName = paymentInfo.DestinationBankName,
                        DestinationBankCountry = paymentInfo.DestinationBankCountry,
                        DestinationBankSwiftCode = paymentInfo.DestinationBankSwiftCode,
                        DestinationBankAccountUniqueId = paymentInfo.DestinationBankAccountUniqueId,
                        RecipientName = paymentInfo.RecipientName,
                        SourceAccountId = model.AccountId
                    };

                    var result = await this.globalTransferHelper.TransferMoneyAsync(serviceModel);

                    if (result != GlobalTransferResult.Succeeded)
                    {
                        return this.PaymentFailed(result == GlobalTransferResult.InsufficientFunds
                            ? NotificationMessages.InsufficientFunds
                            : NotificationMessages.TryAgainLaterError);
                    }

                    // delete cookie to prevent accidental duplicate payments
                    this.Response.Cookies.Delete(PaymentDataCookie);

                    // return signed success response
                    var response = DirectPaymentsHelper.GenerateSuccessResponse(paymentRequest,
                        this.bankConfiguration.Key);

                    return this.Ok(new
                    {
                        success = true,
                        returnUrl = HttpUtility.HtmlEncode(returnUrl),
                        data = response
                    });
                }
                catch
                {
                    return this.PaymentFailed(NotificationMessages.PaymentStateInvalid);
                }
            }

            private IActionResult PaymentFailed(string message)
                => this.Ok(new
                {
                    success = false,
                    errorMessage = message
                });

            private async Task<OwnAccountListingViewModel[]> GetAllAccountsAsync(string userId)
                => (await this.bankAccountService
                        .GetAllAccountsByUserIdAsync<AccountIndexServiceModel>(userId))
                    .Select(this.mapper.Map<OwnAccountListingViewModel>)
                    .ToArray();
        }
    
}
