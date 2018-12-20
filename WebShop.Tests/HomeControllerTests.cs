using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Controllers;
using WebShop.Interfaces;
using WebShop.Interfaces.Clients;
using Xunit;

namespace WebShop.Tests
{
    public class HomeControllerTests
    {
        private HomeController _controller;
        

        public HomeControllerTests()
        {
            var mockService = new Mock<IValuesService>();

            var mokData = new Mock<IProductData>();

            mockService.Setup(c => c.Get()).Returns(new List<string> { "1", "2" });

            _controller = new HomeController(mokData.Object, mockService.Object);
        }

        [Fact]
        public void Index_Method_Returns_View_With_Values()
        {
            // Arrange and act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(
                viewResult.ViewData["Service"]);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Checkout_Returns_View()
        {
            var result = _controller.Checkout();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_Returns_View()
        {
            var result = _controller.Error();
            Assert.IsType<ViewResult>(result);
        }


    }
}
