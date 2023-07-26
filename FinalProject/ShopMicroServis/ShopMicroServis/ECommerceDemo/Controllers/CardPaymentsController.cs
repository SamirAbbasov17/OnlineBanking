namespace ECommerceDemo.Controllers
{
    using System.Threading.Tasks;
    using Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Models;
    using PaymentHelpers;

    public class CardPaymentsController : Controller
    {
        private const string PaymentUnsuccessfulError = "Payment failed. Please check your card details and try again.";
        private readonly CardPaymentsConfiguration cardPaymentsConfiguration;
        private readonly DestinationAccountConfiguration destinationBankAccountConfiguration;

        public CardPaymentsController(
            IOptions<CardPaymentsConfiguration> cardPaymentsConfigurationOptions,
            IOptions<DestinationAccountConfiguration> destinationBankAccountConfigurationOptions)
        {
            this.cardPaymentsConfiguration = cardPaymentsConfigurationOptions.Value;
            this.destinationBankAccountConfiguration = destinationBankAccountConfigurationOptions.Value;
        }

        public async Task<IActionResult> Pay(string name , decimal price)
        {
           
            if (price == 0)
            {
                return this.RedirectToAction("My", "Orders");
            }

            var model = new CardPaymentBindingModel
            {
                ProductName = name,
                ProductPrice = price
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(string name, decimal price, CardPaymentBindingModel model)
        {
            if (price == 0)
            {
                return this.RedirectToAction("My", "Orders");
            }


            if (!this.ModelState.IsValid)
            {
                model.ProductName = name;
                model.ProductPrice = price;

                return this.View(model);
            }

            // create model to send to the CentralApi
            var submitModel = new CardPaymentSubmitModel
            {
                Number = model.Number,
                Name = model.Name,
                ExpiryDate = model.ExpiryDate,
                SecurityCode = model.SecurityCode,
                Amount = price,
                Description = name,
                DestinationBankName = this.destinationBankAccountConfiguration.DestinationBankName,
                DestinationBankCountry = this.destinationBankAccountConfiguration.DestinationBankCountry,
                DestinationBankSwiftCode = this.destinationBankAccountConfiguration.DestinationBankSwiftCode,
                DestinationBankAccountUniqueId =
                    this.destinationBankAccountConfiguration.DestinationBankAccountUniqueId,
                RecipientName = this.destinationBankAccountConfiguration.RecipientName
            };

            // process card
            var success = await CardPaymentsHelper.ProcessPaymentAsync(submitModel,
                this.cardPaymentsConfiguration.CentralApiCardPaymentsUrl);

            // check if the payment is successful
            if (!success)
            {
                this.ModelState.AddModelError(string.Empty, PaymentUnsuccessfulError);

                model.ProductName = name;
                model.ProductPrice = price;

                return this.View(model);
            }

            // mark the payment as completed
            //await this.ordersService.SetPaymentStatus(id, PaymentStatus.Completed);

            return this.RedirectToAction("Index", "Home");
        }
    }
}