using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Entities.Base;

namespace WebShop.Models
{
    public class SectionViewModel: OrderedEntity
    {
        public List<SectionViewModel> ChildSections { get; set; }

        public SectionViewModel ParentSection { get; set; }

        public SectionViewModel ()
        {
            ChildSections = new List<SectionViewModel>();
        }
    }
}
