using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.Entities.Base
{
    public interface INamedEntity : IBaseEntity
    {
        string Name { get; set; }
    }
}
