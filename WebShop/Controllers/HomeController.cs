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
        //ProductContext db;

        //public HomeController(ProductContext context)
        //{
        //    db = context;
        //}

        private readonly List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Ягодный торт",
                Description = "Торт с ягодами на день рождения",
                FullDescription = "Внутри сочный красный бархат, кремчиз и прослойка из свежей клубники",
                Appearance = "Торт оформлен свежими ягодами по краю. В центре надпись с поздравлением",
                Price = 20000
            },
            new Product
            {
                Id = 2,
                Name = "Пряники-снежинки",
                Description = "Имбирные пряники",
                FullDescription = "Нежные имбирные пряники с сахарной глазурью",
                Appearance = "Пряники в виде снежинок подаются в коробке на бумажной подложке",
                Price = 30000
            },
            new Product
            {
                Id = 3,
                Name = "Капкейки",
                Description = "Сладкие капкейки",
                FullDescription = "Шоколадные с апельсиновым курдом и ванильные со сливочной смородиной",
                Appearance = "Оформлены кусочками апельсина с шоколадом и мятой, а так же венскими вафлями",
                Price = 40000
            }
        };
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Catalog()
        {
            return View(_products);
        }

        public IActionResult Details(int id)
        {
            return View(_products.FirstOrDefault(p=>p.Id == id));
        }
    }
}