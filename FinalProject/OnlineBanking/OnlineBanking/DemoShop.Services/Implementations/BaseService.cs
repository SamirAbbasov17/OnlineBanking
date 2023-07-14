using DemoShop.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShop.Services.Implementations
{
    public abstract class BaseService
    {
        protected readonly DemoShopDbContext Context;

        protected BaseService(DemoShopDbContext context)
            => this.Context = context;

        public static bool IsEntityStateValid(object model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(model, validationContext, validationResults,
                true);
        }
    }
}
