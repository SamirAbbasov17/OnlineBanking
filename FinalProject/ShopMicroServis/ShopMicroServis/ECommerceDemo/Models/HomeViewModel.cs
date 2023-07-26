namespace ECommerceDemo.Models
{
    using System.Collections.Generic;

    public class HomeViewModel
    {
        public IEnumerable<ProductDetailsViewModel> Products { get; set; }
    }
}