using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Domain.Entities;
using WebShop.Infrastructure.Interfaces;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;

        public CatalogController(IProductData productData) => _productData = productData;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shop(int? sectionId, int? eventId)
        {
            var products = _productData.GetProducts(new ProductFilter() { SectionId = sectionId, EventId = eventId });
            var model = new CatalogViewModel
            {
                EventId = eventId,
                SectionId = sectionId,
                Products = products.Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    FullDescription = p.FullDescription,
                    Appearance = p.Appearance,
                    Price = p.Price,
                    New = p.New,
                    Sale = p.Sale,
                    ImageUrl = p.ImageUrl,
                    Order = p.Order
                }).OrderBy(p => p.Order).ToList()
            };
            return View(model);
        }

        public IActionResult Details(int id)
        {

            var porduct = _productData.GetById(id);
            if (ReferenceEquals(porduct, null))
                return RedirectToAction("CustomNotFound", "Error");

            return View(new ProductViewModel() {
                Id = porduct.Id,
                Name = porduct.Name,
                Description = porduct.Description,
                FullDescription = porduct.FullDescription,
                Appearance = porduct.Appearance,
                Price = porduct.Price,
                New = porduct.New,
                Sale = porduct.Sale,
                ImageUrl = porduct.ImageUrl,
                Order = porduct.Order
            });
        }

        public IActionResult Edite(int? id)
        {
            Product product;
            if (id.HasValue)
            {
                product = _productData.GetById(id.Value);
                if (ReferenceEquals(product, null)) return NotFound();
            }
            else
            {
                product = new Product();
            }
            return View(product);

        }

        [HttpPost]
        public IActionResult Edite(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id > 0)
                {
                    var dbItem = _productData.GetById(product.Id);
                    if (ReferenceEquals(dbItem, null)) return NotFound();
                    dbItem.ImageUrl = product.ImageUrl;
                    dbItem.Name = product.Name;
                    dbItem.Price = product.Price;
                    dbItem.Sale = product.Sale;
                    dbItem.New = product.New;
                    dbItem.FullDescription = product.FullDescription;
                    dbItem.Description = product.Description;
                    dbItem.Appearance = product.Appearance;
                    _productData.Update(dbItem);
                }
                else
                {
                    _productData.AddNew(product);
                }
                return RedirectToAction(nameof(Shop));
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            _productData.Delete(id);
            return RedirectToAction(nameof(Shop));
        }
    }
}