using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Infrastructure.Interfaces;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductData _productData;

        public HomeController(IProductData productData) => _productData = productData;
           

        

        public IActionResult Index()
        {
            return View(_productData.GetAll());
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Shop()
        {
            return View(_productData.GetAll());
        }

        public IActionResult Catalog()
        {
            return View(_productData.GetAll());
        }

        public IActionResult Details(int id)
        {
            
            var porduct = _productData.GetById(id);
            if (ReferenceEquals(porduct, null))
                return RedirectToAction("CustomNotFound", "Error");
            
            return View(porduct);
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

        public IActionResult Edite(int? id)
        {
            Product product;
            if(id.HasValue)
            {
                product = _productData.GetById(id.Value);
                if(ReferenceEquals(product, null)) return NotFound();
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
            if(product.Id > 0)
            {
                var dbItem = _productData.GetById(product.Id);
                if (ReferenceEquals(dbItem, null)) return NotFound();
                dbItem.Image = product.Image;
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

        public IActionResult Delete(int id)
        {
            _productData.Delete(id);
            return RedirectToAction(nameof(Shop));
        }

    }
}