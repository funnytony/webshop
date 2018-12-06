using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Infrastructure.Interfaces;
using WebShop.Interfaces.Clients;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductData _productData;

        private readonly IValuesService _valuesService;

        public HomeController(IProductData productData, IValuesService valuesService)
        {
            _productData = productData;
            _valuesService = valuesService;
        }
           

        

        public IActionResult Index()
        {
            var value = _valuesService.Get();
            ViewBag.Service = value;
            var products = _productData.GetAll().Select(p => new ProductViewModel() {
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
            }).ToList();
            return View(products);
        }

        public IActionResult Cart()
        {
            return View();
        }

        

        public IActionResult Catalog()
        {
            return View(_productData.GetAll());
        }

        

        public IActionResult Checkout()
        {
            return View();            
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AddToCart(int? id)
        {
            return RedirectToAction("Index");
        }

        

    }
}