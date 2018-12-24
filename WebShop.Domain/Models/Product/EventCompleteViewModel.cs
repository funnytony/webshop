using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Models;

namespace WebShop.Domain.Models.Product
{
    public class EventCompleteViewModel
    {
        public IEnumerable<EventViewModel> Events { get; set; }

        public int? CurrentEventId { get; set; }
    }
}
