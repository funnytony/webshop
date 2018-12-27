using WebShop.Domain.Entities.Base;

namespace WebShop.Domain.DTO.Order
{
    public class OrderItemDto : BaseEntity
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}