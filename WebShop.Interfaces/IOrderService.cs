using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Models.Cart;
using WebShop.Models.Order;

namespace WebShop.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetUserOrders(string userName);

        Order GetOrderById(int id);

        Order CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName);

    }
}
