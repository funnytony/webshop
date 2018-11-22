using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Domain.Entities;

namespace WebShop.DAL.Context
{
    public class WebShopContext: DbContext
    {
        public WebShopContext(DbContextOptions options) : base(options)
        {
           
        }

         public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Event> Events { get; set; }
    }
}
