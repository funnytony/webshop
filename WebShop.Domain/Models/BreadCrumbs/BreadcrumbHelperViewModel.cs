using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.Models.BreadCrumbs
{
    public class BreadcrumbHelperViewModel
    {
        public BreadCrumbType Type { get; set; }

        public BreadCrumbType FromType { get; set; }

        public int Id { get; set; }
    }
}
