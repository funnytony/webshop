using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.Cart
{
    public class CartViewModel
    {
        public Dictionary<ProductViewModel, int> Items { get; set; }

        public int ItemsCount
        {
            get
            {
                return Items?.Sum(x => x.Value) ?? 0;
            }
        }

        public decimal PriceSum
        {
            get
            {
                return Items?.Sum(x => x.Value * x.Key.Price) ?? 0;
            }
        }
    }
}
