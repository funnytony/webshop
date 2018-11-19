using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Infrastructure.Interfaces;
using WebShop.Models;

namespace WebShop.ViewComponents
{
    public class EventsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public EventsViewComponent(IProductData productData) => _productData = productData;

        public IViewComponentResult Invoke()
        {
            var events = GetEvents();
            return View(events);
        }

        private IEnumerable<EventViewModel> GetEvents()
        {
            var events = _productData.GetEvents();
            return events.Select(e => new EventViewModel {
                Id = e.Id,
                Name = e.Name,
                Order = e.Order,
                ProductsCount = 0
            }).OrderBy(e=>e.Order).ToList();
        }
    }
}
