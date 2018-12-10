using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Domain.Entities.Base;

namespace WebShop.Domain.Entities
{
    public class Event: OrderedEntity
    {
        public virtual ICollection<Product> Products { get; set; }
    }
}
