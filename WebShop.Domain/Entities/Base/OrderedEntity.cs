using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebShop.Domain.Entities.Base
{
    public class OrderedEntity : NamedEntity, IOrderedEntity
    {
        [Required, Display(Name = "Порядок")]
        public int Order { get; set; }
    }
}
