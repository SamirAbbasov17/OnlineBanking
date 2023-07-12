using MainApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApi.Data
{
    public class MainApiDbContext : DbContext
    {
        public MainApiDbContext(DbContextOptions<MainApiDbContext> options) : base(options)
        {
        }

        public DbSet<Bank> Banks { get; set; }
    }
}
