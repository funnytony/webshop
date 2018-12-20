using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Entities.Base;

namespace WebShop.Models
{
    public class EventViewModel : OrderedEntity
    {
        public int ProductsCount { get; set; }
    }
}
