namespace ECommerceDemo.Controllers
{
    using System.Threading.Tasks;
    using Configuration;
    using ECommerceDemo.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Models;
    using Newtonsoft.Json;
    using PaymentHelpers;

    [Authorize]
    public class DirectPaymentsController : Controller
    {
        private const string ReturnPath = "DirectPayments/ReceiveConfirmation/";
        private const string PaymentDataFormKey = "data";
        private readonly DestinationAccountConfiguration destinationBankAccountConfiguration;
        HttpClientHandler clientHandler;
        HttpClient client;
        private readonly DirectPaymentsConfiguration directPaymentsConfiguration;

        public DirectPaymentsController(
            IOptions<DirectPaymentsConfiguration> directPaymentsConfigurationOptions,
            IOptions<DestinationAccountConfiguration> destinationBankAccountConfigurationOptions)
        {
            this.directPaymentsConfiguration = directPaymentsConfigurationOptions.Value;
            this.destinationBankAccountConfiguration = destinationBankAccountConfigurationOptions.Value;
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
        }

        public async Task<IActionResult> Pay(string name , decimal price)
        {
            try
            { 
                if (price == 0)
                {
                    return this.RedirectToAction("My", "Orders");
                }

                var paymentInfo = new
                {
                    Amount = price,
                    Description = name,
                    this.destinationBankAccountConfiguration.DestinationBankName,
                    this.destinationBankAccountConfiguration.DestinationBankCountry,
                    this.destinationBankAccountConfiguration.DestinationBankSwiftCode,
                    this.destinationBankAccountConfiguration.DestinationBankAccountUniqueId,
                    this.destinationBankAccountConfiguration.RecipientName,

                    // ! PaymentInfo can also contain custom properties
                    // ! that will be returned on payment completion

                    // ! OrderId is a custom property and is not required
                   
                };

                // generate the returnUrl where the payment result will be received
                var returnUrl = this.directPaymentsConfiguration.SiteUrl + ReturnPath;

                // generate signed payment request
                var paymentRequest = DirectPaymentsHelper.GeneratePaymentRequest(
                    paymentInfo, this.directPaymentsConfiguration.SiteKey, returnUrl);

                // redirect the user to the CentralApi for payment processing
                var paymentPostRedirectModel = new PaymentPostRedirectModel
                {
                    Url = this.directPaymentsConfiguration.CentralApiPaymentUrl,
                    PaymentDataFormKey = PaymentDataFormKey,
                    PaymentData = paymentRequest
                };

                return this.View("PaymentPostRedirect", paymentPostRedirectModel);
            }
            catch
            {
                return this.RedirectToAction("My", "Orders");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ReceiveConfirmation(string data)
        {
            if (data == null)
            {
                return this.BadRequest();
            }

            dynamic paymentInfo = DirectPaymentsHelper.ProcessPaymentResult(
                data,
                this.directPaymentsConfiguration.SiteKey,
                this.directPaymentsConfiguration.CentralApiPublicKey);

            if (paymentInfo == null)
            {
                // if the returned PaymentInfo is null, it has not been parsed or verified successfully
                return this.BadRequest();
            }

            // extract the orderId from the PaymentInfo
            string orderId;

            try
            {
                orderId = paymentInfo.OrderId;
            }
            catch
            {
                return this.BadRequest();
            }

            if (orderId == null)
            {
                return this.BadRequest();
            }

            // find the order in the database
            //var order = await this.ordersService.GetByIdAsync(orderId);

            //// check if the order does not exist or the payment has already been completed 
            //if (order == null || order.PaymentStatus != PaymentStatus.Pending)
            //{
            //    return this.RedirectToAction("My", "Orders");
            //}

            //// mark the payment as completed
            //await this.ordersService.SetPaymentStatus(orderId, PaymentStatus.Completed);

            return this.RedirectToAction("My", "Orders");
        }
    }
}