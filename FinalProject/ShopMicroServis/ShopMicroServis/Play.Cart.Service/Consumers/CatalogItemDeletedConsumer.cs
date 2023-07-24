using System.Threading.Tasks;
using MassTransit;
using Play.Cart.Service.Entities;
using Play.Common;
using Play.Shop.Contracts;

namespace Play.Cart.Service.Consumers
{
    public class CatalogItemDeletedConsumer : IConsumer<ShopItemDeleted>
    {
        private readonly IRepository<ShopItem> repository;

        public CatalogItemDeletedConsumer(IRepository<ShopItem> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<ShopItemDeleted> context)
        {
            var message = context.Message;

            var item = await repository.GetAsync(message.ItemId);

            if (item == null)
            {
                return;
            }

            await repository.RemoveAsync(message.ItemId);
        }
    }
}