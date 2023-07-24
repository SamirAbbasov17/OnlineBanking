using System.Threading.Tasks;
using MassTransit;
using Play.Cart.Service.Entities;
using Play.Common;
using Play.Shop.Contracts;

namespace Play.Cart.Service.Consumers
{
    public class CatalogItemCreatedConsumer : IConsumer<ShopItemCreated>
    {
        private readonly IRepository<ShopItem> repository;

        public CatalogItemCreatedConsumer(IRepository<ShopItem> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<ShopItemCreated> context)
        {
            var message = context.Message;

            var item = await repository.GetAsync(message.ItemId);

            if (item != null)
            {
                return;
            }

            item = new ShopItem
            {
                Id = message.ItemId,
                Name = message.Name,
                Description = message.Description,
                Price = message.Price,
                Image = message.Image
            };

            await repository.CreateAsync(item);
        }
    }
}