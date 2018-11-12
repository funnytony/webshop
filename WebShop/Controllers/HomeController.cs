using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        ProductContext db;

        public HomeController(ProductContext context)
        {
            db = context;
        }       

        

        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public IActionResult Cart()
        {
            return View(db.Carts.ToList());
        }

        public IActionResult Shop()
        {
            return View(db.Products.ToList());
        }

        public IActionResult Catalog()
        {
            return View(db.Products.ToList());
        }

        public IActionResult Details(/*int id*/)
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View(db.Carts.ToList());            
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AddToCart(int? id)
        {
            Product product = db.Products.FirstOrDefault(p => p.Id == id);
            if(product!= null)
            {
                var cart = db.Carts.ToList().Find(c=>c.Product == product);
                if (cart != null)
                {
                    cart.Amount++;
                    db.Carts.Update(cart);
                }
                db.Carts.Add(new Cart { Product = product, Amount = 1 });
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}