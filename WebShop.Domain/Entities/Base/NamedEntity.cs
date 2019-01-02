using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebShop.Domain.Entities.Base
{
    public class NamedEntity : BaseEntity, INamedEntity
    {
        [Required, Display(Name = "Название")]
        public string Name { get; set; }        
    }
}
