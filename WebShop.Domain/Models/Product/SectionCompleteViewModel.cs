using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Models;

namespace WebShop.Domain.Models.Product
{
    public class SectionCompleteViewModel
    {
        public IEnumerable<SectionViewModel> Sections { get; set; }

        public int? CurrentParentSectionId { get; set; }

        public int? CurrentSectionId { get; set; }
    }
}
