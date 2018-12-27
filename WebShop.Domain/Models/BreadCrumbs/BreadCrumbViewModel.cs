using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.Models.BreadCrumbs
{
    public class BreadCrumbViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public BreadCrumbType BreadCrumbType { get; set; }

    }
}
