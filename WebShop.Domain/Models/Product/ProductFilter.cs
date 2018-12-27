using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class ProductFilter
    {
        public int? SectionId { get; set; }

        public int? EventId { get; set; }

        public List<int> Ids { get; set; }

        public int Page { get; set; }

        public int? PageSize { get; set; }

    }
}
