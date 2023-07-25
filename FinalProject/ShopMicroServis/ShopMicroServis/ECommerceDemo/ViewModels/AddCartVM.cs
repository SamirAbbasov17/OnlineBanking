namespace ECommerceDemo.ViewModels
{
    public class AddCartVM
    {
        public Guid UserId { get; set; }

        public Guid CatalogItemId { get; set; }

        public int Quantity { get; set; }
    }
}
