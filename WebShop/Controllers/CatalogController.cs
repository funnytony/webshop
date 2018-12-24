using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Domain;
using WebShop.Domain.DTO.Product;
using WebShop.Domain.Entities;
using WebShop.Domain.Models.BreadCrumbs;
using WebShop.Domain.Models.Product;
using WebShop.Interfaces;
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
            var sections = _productData.GetSections();
            var events = _productData.GetEvents();
            var title = "Сладости";
            if(sectionId.HasValue)
            {
                title = sections.FirstOrDefault(s => s.Id == sectionId)?.Name;
            }
            else if(eventId.HasValue)
            {
                title = events.FirstOrDefault(e => e.Id == eventId)?.Name;
            }
            var model = new CatalogViewModel
            {
                EventId = eventId,
                SectionId = sectionId,
                Products = new ProductItemsViewModel()
                {
                    Title = title,
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
                        Order = p.Order,
                        Event = p.Event != null ? p.Event.Name : string.Empty
                    }).OrderBy(p => p.Order).ToList()
                },
                BreadcrumbHelper = BreadCrumbLogic()
                
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
                Order = porduct.Order,
                Event = porduct.Event != null? porduct.Event.Name : string.Empty,
                BreadcrumbHelper = BreadCrumbLogic()
            });
        }
        [Authorize(Roles = WebShopConstants.Roles.Admin)]
        [HttpGet]
        public IActionResult Edite(int? id)
        {
            
            ProductViewModel model;
            if (id.HasValue)
            {
                var product = _productData.GetById(id.Value);
                if (ReferenceEquals(product, null)) return NotFound();
                model = new ProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    FullDescription = product.FullDescription,
                    Appearance = product.Appearance,
                    ImageUrl = product.ImageUrl,
                    New = product.New,
                    Sale = product.Sale,
                    Price = product.Price,
                    Order = product.Order
                };
            }
            else
            {
                model = new ProductViewModel();
            }
            return View(model);

        }

        [Authorize(Roles = WebShopConstants.Roles.Admin)]
        [HttpPost]
        public IActionResult Edite(ProductViewModel product)
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
                    ProductDto dbItem = new ProductDto()
                    {
                        Name = product.Name,
                        Description = product.Description,
                        FullDescription = product.FullDescription,
                        Appearance = product.Appearance,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        New = true,
                        Sale = false
                    };
                    _productData.AddNew(dbItem);
                }
                return RedirectToAction(nameof(Shop));
            }
            return View(product);
        }

        [Authorize(Roles = WebShopConstants.Roles.Admin)]
        public IActionResult Delete(int id)
        {
            _productData.Delete(id);
            return RedirectToAction(nameof(Shop));
        }

        private BreadcrumbHelperViewModel BreadCrumbLogic()
        {
            var type = Request.Query.ContainsKey("sectionId") ?
                BreadCrumbType.Section : Request.Query.ContainsKey("eventId") ?
                    BreadCrumbType.Event : BreadCrumbType.None;

            // Устанавливаем дефолтное значение источника
            var fromType = BreadCrumbType.Section;

            // Если это метод деталей товара
            if ((string)RouteData.Values["action"] == "Details")
            {
                // Устанавливаем тип товар
                type = BreadCrumbType.Item;
            }

            var id = 0;

            switch (type)
            {
                case BreadCrumbType.None:
                    break;
                case BreadCrumbType.Section:
                    id = int.Parse(Request.Query["sectionId"].ToString());
                    break;
                case BreadCrumbType.Event:
                    id = int.Parse(Request.Query["eventId"].ToString());
                    break;
                case BreadCrumbType.Item:
                    // Если есть ключ того, что пришли с бренда, ставим источник – бренд
                    if (Request.Query.ContainsKey("fromEvent"))
                    {
                        fromType = BreadCrumbType.Event;
                    }
                    id = int.Parse(RouteData.Values["id"].ToString());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new BreadcrumbHelperViewModel()
            {
                Id = id,
                FromType = fromType,
                Type = type
            };
        }

    }
}