using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class CatalogViewModel
    {
        public int? EventId { get; set; }

        public int? SectionId { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
