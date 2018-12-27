using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Domain.DTO.Order;
using WebShop.Infrastructure.Interfaces;
using WebShop.Interfaces;
using WebShop.Models.Order;

namespace WebShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public IActionResult Details()
        {
            var model = new DetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = new OrderViewModel()                
            };
            return View("Details", model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Checkout(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var orderResult = _orderService.CreateOrder(new CreateOrderModel()
                {
                    OrderViewModel = model,
                    OrderItems = _cartService.TransformCart().Items.Select(o=> new OrderItemDto()
                    {
                        Price = o.Key.Price,
                        Quantity = o.Value
                    }).ToList()
                }, User.Identity.Name);
                _cartService.RemoveAll();
                return RedirectToAction("OrderConfirmed", new { id = orderResult.Id });
            }

            var detailsModel = new DetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = model
            };
            return View("Details", detailsModel);
            
        }

        public IActionResult DecrementFromCart(int id)
        {
            _cartService.DecrementFromCart(id);
            //return RedirectToAction("Details");
            return Json(new { id, message = "Количество товара уменьшено на 1" });
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            //return RedirectToAction("Details");
            return Json(new { id, message = "Товар удален из корзины" });
        }

        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            //return RedirectToAction("Details");
            return Json(new { message = "Все товары удалены из корзины" });

        }

        public IActionResult AddToCart(int id)
        {
            _cartService.AddToCart(id);

            return Json(new { id, message = "Товар добавлен в корзину" });

            //if (Url.IsLocalUrl(returnUrl))
            //    return Redirect(returnUrl);

            //return RedirectToAction("Index", "Home");
        }

        public IActionResult GetCartView()
        {
            return ViewComponent("Cart");
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

    }
}