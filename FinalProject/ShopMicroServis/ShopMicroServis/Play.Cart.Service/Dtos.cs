using System;

namespace Play.Cart.Service.Dtos
{
    public record GrantItemsDto(Guid UserId, Guid CatalogItemId, int Quantity);

    public record CartItemDto(Guid ShopItemId, string Name, string Description,decimal Price,string Image, int Quantity);

    public record ShopItemDto(Guid Id, string Name, string Description,decimal Price,string Image);
}