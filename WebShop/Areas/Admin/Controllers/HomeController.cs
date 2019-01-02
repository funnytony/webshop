using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebShop.Domain;
using WebShop.Domain.DTO.Product;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = WebShopConstants.Roles.Admin)]
    public class HomeController : Controller
    {
        private readonly IProductData _productData;

        public HomeController(IProductData productData) => _productData = productData;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList()
        {
            var products = _productData.GetProducts(new ProductFilter());
            return View(products);
        }


        public IActionResult Edit(int? id)
        {
            var notParentSections = _productData.GetSections().Where(s => s.ParentId != null);
            var events = _productData.GetEvents();

            if (!id.HasValue)
            {
                return View(new ProductViewModel()
                {
                    Sections = new SelectList(notParentSections, "Id", "Name"),
                    Events = new SelectList(events, "Id", "Name")
                });
            }


            var product = _productData.GetById(id.Value);
            if (product == null)
                return NotFound();

            return View(new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Section = product.Section.Name,
                SectionId = product.Section.Id,
                Event = product.Event?.Name,
                EventId = product.Event?.Id,
                Events = new SelectList(events, "Id", "Name", product.Event?.Id),
                Sections = new SelectList(notParentSections, "Id", "Name", product.Section.Id)
            });
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            var notParentSections = _productData.GetSections().Where(s => s.ParentId != null);
            var events = _productData.GetEvents();
            if (ModelState.IsValid)
            {
                var productDto = new ProductDto()
                {
                    Id = model.Id,
                    ImageUrl = model.ImageUrl,
                    Name = model.Name,
                    Order = model.Order,
                    Price = model.Price,
                    Event = model.EventId.HasValue
                        ? new EventDto()
                        {
                            Id = model.EventId.Value
                        }
                        : null,
                    Section = new SectionDto()
                    {
                        Id = model.SectionId.Value
                    }
                };
                if (model.Id > 0)
                {
                    _productData.Update(productDto);
                }
                else
                {
                    _productData.AddNew(productDto);
                }
                return RedirectToAction(nameof(ProductList));
            }

            model.Events = new SelectList(events, "Id", "Name", model.EventId);
            model.Sections = new SelectList(notParentSections, "Id", "Name", model.SectionId);

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            _productData.Delete(id);
            return RedirectToAction(nameof(ProductList));
        }

    }
}