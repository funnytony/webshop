using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShop.Models
{
    public class Cart
    {
        public int Id { get; set; }//код записи

        public Product Product { get; set; }//продукт в корзине

        public int Amount { get; set; }//количество
    }
}
