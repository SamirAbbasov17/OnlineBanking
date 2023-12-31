using System;
using Play.Common;

namespace Play.Cart.Service.Entities
{
    public class CartItem : IEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid CatalogItemId { get; set; }

        public int Quantity { get; set; }
    }
}