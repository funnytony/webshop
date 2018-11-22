using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Domain.Entities.Base;

namespace WebShop.Domain.Entities
{
    public class Section : OrderedEntity
    {
        public int? ParentId { get; set; }


        [ForeignKey("ParentId")]
        public virtual Section ParentSection { get; set; }
    }
}
