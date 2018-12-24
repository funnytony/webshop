using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Models.BreadCrumbs;
using WebShop.Domain.Models.Product;

namespace WebShop.Models
{
    public class CatalogViewModel
    {
        public int? EventId { get; set; }

        public int? SectionId { get; set; }

        public ProductItemsViewModel Products { get; set; }

        public BreadcrumbHelperViewModel BreadcrumbHelper { get; set; }
    }
}
