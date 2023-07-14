using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShop.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "1000000000")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageUrl { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
