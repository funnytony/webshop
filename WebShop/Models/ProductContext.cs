using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Cart> Carts { get; set; }
        
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
