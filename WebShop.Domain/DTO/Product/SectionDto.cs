using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Domain.Entities.Base;

namespace WebShop.Domain.DTO.Product
{
    public class SectionDto : OrderedEntity
    {
        public int? ParentId { get; set; }
    }
}
