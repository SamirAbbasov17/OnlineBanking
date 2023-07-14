using DemoShop.Services.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShop.Services.Interfaces
{
    public interface IProductsService
    {
        Task CreateAsync(ProductCreateServiceModel model);
        Task<IEnumerable<ProductDetailsServiceModel>> GetAllAsync();
    }
}
