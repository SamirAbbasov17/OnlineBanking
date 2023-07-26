namespace ECommerceDemo.ViewModels
{
    public class CartVM
    {
        public Guid ShopItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid? UserId { get; set; }
    }
}
