using MainApi.Infrastructure.Helpers;
using MainApi.Models;
using MainApi.Services.Bank;
using MainApi.Services.Models.Banks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace MainApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CardPaymentsController : ControllerBase
    {
        private readonly IBanksService banksService;
        private readonly MainApiConfiguration configuration;

        public CardPaymentsController(IBanksService banksService, IOptions<MainApiConfiguration> configuration)
        {
            this.banksService = banksService;
            this.configuration = configuration.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CardPaymentDto model)
        {
            try
            {
                var first3Digits = model.Number.Substring(0, 3);
                var bank = await this.banksService
                    .GetBankByBankIdentificationCardNumbersAsync<BankPaymentServiceModel>(first3Digits);
                if (bank?.CardPaymentUrl == null)
                {
                    return this.BadRequest();
                }

                var encryptedAndSignedData = TransactionHelper.SignAndEncryptData(model, this.configuration.Key, bank.ApiKey);
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);
                var request = await client.PostAsJsonAsync(bank.CardPaymentUrl, encryptedAndSignedData);

                if (request.StatusCode != HttpStatusCode.OK)
                {
                    return this.BadRequest();
                }

                return this.Ok();
            }
            catch
            {
                return this.BadRequest();
            }
        }
    }
}
