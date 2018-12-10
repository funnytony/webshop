using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.DTO.Order;
using WebShop.Domain.Entities;
using WebShop.Models.Cart;
using WebShop.Models.Order;

namespace WebShop.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetUserOrders(string userName);

        OrderDto GetOrderById(int id);

        OrderDto CreateOrder(CreateOrderModel orderModel, string userName);

    }
}
