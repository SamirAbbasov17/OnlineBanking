using System.Threading.Tasks;
using MassTransit;
using Play.Cart.Service.Entities;
using Play.Common;
using Play.Shop.Contracts;

namespace Play.Cart.Service.Consumers
{
    public class CatalogItemUpdatedConsumer : IConsumer<ShopItemUpdated>
    {
        private readonly IRepository<ShopItem> repository;

        public CatalogItemUpdatedConsumer(IRepository<ShopItem> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<ShopItemUpdated> context)
        {
            var message = context.Message;

            var item = await repository.GetAsync(message.ItemId);

            if (item == null)
            {
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
            else
            {
                item.Name = message.Name;
                item.Description = message.Description;
                item.Price = message.Price;
                item.Image = message.Image;

                await repository.UpdateAsync(item);
            }
        }
    }
}