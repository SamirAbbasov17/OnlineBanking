using Play.Shop.Service.Dtos;
using Play.Shop.Service.Entities;

namespace Play.Shop.Service
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.Image);
        }
    }
}