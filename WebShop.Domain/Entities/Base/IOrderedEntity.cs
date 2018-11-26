using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.Entities.Base
{
    public interface IOrderedEntity : INamedEntity
    {
        int Order { get; set; }
    }
}
