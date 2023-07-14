using DemoShop.Models;
using DemoShop.Services.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShop.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<string> CreateAsync(OrderCreateServiceModel model);
        Task<OrderDetailsServiceModel> GetByIdAsync(string id);
        Task<IEnumerable<OrderDetailsServiceModel>> GetAllForUserAsync(string userName);
        Task SetPaymentStatus(string orderId, PaymentStatus paymentStatus);
    }
}
