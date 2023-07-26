namespace ECommerceDemo.Models
{
    using ECommerceDemo.Models;
    using System;

    public class OrderDetailsViewModel
    {
        public string Id { get; set; }

        public string ProductName { get; set; }

        public string ProductImageUrl { get; set; }

        public decimal ProductPrice { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }
}