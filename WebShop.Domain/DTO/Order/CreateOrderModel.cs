using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Models.Order;

namespace WebShop.Domain.DTO.Order
{
    public class CreateOrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }

    }
}
