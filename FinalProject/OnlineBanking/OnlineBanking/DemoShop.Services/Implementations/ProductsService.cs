using DemoShop.Data;
using DemoShop.Models;
using DemoShop.Services.Interfaces;
using DemoShop.Services.Models.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShop.Services.Implementations
{
    public class ProductsService : BaseService, IProductsService
    {
        public ProductsService(DemoShopDbContext context) : base(context) { }

        public async Task CreateAsync(ProductCreateServiceModel model)
        {
            if (!IsEntityStateValid(model))
            {
                return;
            }

            var dbModel = new Product
            {
                Name = model.Name,
                Price = model.Price,
                ImageUrl = model.ImageUrl
            };

            await this.Context.Products.AddAsync(dbModel);
            await this.Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDetailsServiceModel>> GetAllAsync()
        {
            var products = await this.Context
                .Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Select(p => new ProductDetailsServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                })
                .ToArrayAsync();

            return products;
        }
    }
}
