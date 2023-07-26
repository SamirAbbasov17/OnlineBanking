using ECommerceDemo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDemo.Data
{
    public class ECommerceDataDbContext : IdentityDbContext<ECommerceDemoUser>
    {
        public ECommerceDataDbContext(DbContextOptions<ECommerceDataDbContext> options)
           : base(options)
        {
        }

      
    }
}
