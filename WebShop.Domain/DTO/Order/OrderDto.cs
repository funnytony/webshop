using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Domain.Entities.Base;

namespace WebShop.Domain.DTO.Order
{
    public class OrderDto:NamedEntity
    {
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }

    }
}
