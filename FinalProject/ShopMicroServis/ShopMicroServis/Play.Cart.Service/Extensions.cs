using Play.Cart.Service.Dtos;
using Play.Cart.Service.Entities;

namespace Play.Cart.Service
{
    public static class Extensions
    {
        public static CartItemDto AsDto(this CartItem item, string name, string description,decimal price,string image)
        {
            return new CartItemDto(item.CatalogItemId, name, description,price,image,item.Quantity);
        }
    }
}