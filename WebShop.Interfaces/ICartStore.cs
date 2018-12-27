using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Models.Cart;

namespace WebShop.Interfaces
{
    public interface ICartStore
    {
        Cart Cart { get; set; }
    }
}
