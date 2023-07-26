namespace ECommerceDemo.PaymentHelpers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Models;

    public static class CardPaymentsHelper
    {
        public static async Task<bool> ProcessPaymentAsync(CardPaymentSubmitModel model, string centralApiSubmitUrl)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);
            //var client = new HttpClient();
            var request = await client.PostAsJsonAsync(centralApiSubmitUrl, model);

            return request.StatusCode == HttpStatusCode.OK;
        }
    }
}