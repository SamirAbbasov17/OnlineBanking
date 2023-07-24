using System;

namespace Play.Shop.Contracts
{
    public record ShopItemCreated(Guid ItemId, string Name, string Description , decimal Price, string Image);

    public record ShopItemUpdated(Guid ItemId, string Name, string Description, decimal Price, string Image);

    public record ShopItemDeleted(Guid ItemId);
}