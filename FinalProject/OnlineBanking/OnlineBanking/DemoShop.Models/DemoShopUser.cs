using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DemoShop.Models
{
    public class DemoShopUser : IdentityUser
    {
        public ICollection<Order> Orders { get; set; }
    }
}
