using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Models.Product;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.ViewComponents
{
    public class EventsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public EventsViewComponent(IProductData productData) => _productData = productData;

        public async Task<IViewComponentResult> InvokeAsync(string eventId)
        {
            int.TryParse(eventId, out var eventIdInt);
            var events = GetEvents();
            return View(new EventCompleteViewModel() {Events = events, CurrentEventId = eventIdInt });
        }

        private IEnumerable<EventViewModel> GetEvents()
        {
            var events = _productData.GetEvents();
            return events.Select(e => new EventViewModel {
                Id = e.Id,
                Name = e.Name,
                Order = e.Order,
                ProductsCount = _productData.GetEventProductCount(e.Id)
            }).OrderBy(e=>e.Order).ToList();
        }
    }
}
