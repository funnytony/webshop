using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Domain;
using WebShop.Infrastructure.Interfaces;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = WebShopConstants.Roles.Admin)]
    public class HomeController : Controller
    {
        private readonly IProductData _producData;

        public HomeController(IProductData productData) => _producData = productData;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList()
        {
            var products = _producData.GetProducts(new ProductFilter());
            return View(products);
        }

    }
}