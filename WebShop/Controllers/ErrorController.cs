using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebShop.Controllers
{
    public class ErrorController : Controller
    {

        public IActionResult ErrorStatus(string id)
        {
            if (id == "404")
                return RedirectToAction("CustomNotFound");
            return Content($"Статуcный код ошибки: {id}");
        }
        
        public IActionResult Error()
        {
            return View();
        }

        public IActionResult CustomNotFound()
        {
            return View();
        }
    }
}