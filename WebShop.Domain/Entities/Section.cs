using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Domain.Entities.Base;

namespace WebShop.Domain.Entities
{
    public class Section : OrderedEntity
    {
        public int? ParentId { get; set; }
    }
}
