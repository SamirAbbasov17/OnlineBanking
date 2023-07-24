using Play.Cart.Service.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace Play.Cart.Service.Clients
{
    public class ShopClient
    {
        private readonly HttpClient httpClient;

        public ShopClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<ShopItemDto>> GetCatalogItemsAsync()
        {
            var items = await httpClient.GetFromJsonAsync<IReadOnlyCollection<ShopItemDto>>("/items");
            return items;
        }
    }
}