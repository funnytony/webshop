using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Models;

namespace WebShop.Domain.Models.Product
{
    public class ProductItemsViewModel
    {

        public string Title { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
