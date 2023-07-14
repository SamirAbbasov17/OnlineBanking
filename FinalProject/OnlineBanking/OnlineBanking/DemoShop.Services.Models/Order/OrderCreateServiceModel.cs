using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShop.Services.Models.Order
{
    public class OrderCreateServiceModel
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
