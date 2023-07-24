using System;
using Play.Common;

namespace Play.Cart.Service.Entities
{
    public class ShopItem : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}