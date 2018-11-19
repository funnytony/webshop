using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.Entities.Base
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
