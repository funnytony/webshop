using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShop.Controllers;
using WebShop.Domain.DTO.Product;
using WebShop.Interfaces;
using WebShop.Models;
using Xunit;

namespace WebShop.Tests
{
    public class CatalogControllerTests
    {
        [Fact]
        public void ProductDetails_Returns_View_With_Correct_Item()
        {
            // Arrange
            var productMock = new Mock<IProductData>();
            var configurationMock = new Mock<IConfiguration>();
            productMock.Setup(p => p.GetById(It.IsAny<int>())).Returns(new ProductDto()
            {
                Id = 1,
                Name = "Test",
                ImageUrl = "TestImage.jpg",
                Order = 0,
                Price = 10,
                Event = new EventDto()
                {
                    Id = 1,
                    Name = "TestEvent"
                }
            });
            var controller = new CatalogController(productMock.Object, configurationMock.Object);

            // Act
            var result = controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
            Assert.Equal("Test", model.Name);
            Assert.Equal(10, model.Price);
            Assert.Equal("TestEvent", model.Event);
        }

        [Fact]
        public void ProductDetails_Returns_NotFound()
        {
            // Arrange
            var productMock = new Mock<IProductData>();
            var configurationMock = new Mock<IConfiguration>();
            productMock.Setup(p => p.GetById(It.IsAny<int>())).Returns((ProductDto)null);
            var controller = new CatalogController(productMock.Object, configurationMock.Object);

            // Act
            var result = controller.Details(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirectToActionResult.ControllerName);
            Assert.Equal("CustomNotFound", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Shop_Method_Returns_Correct_View()
        {
            // Arrange
            var productMock = new Mock<IProductData>();
            var configurationMock = new Mock<IConfiguration>();
            productMock.Setup(p => p.GetProducts(It.IsAny<ProductFilter>()).Products).Returns(new List<ProductDto>()
            {
                new ProductDto()
                {
                    Id = 1,
                    Name = "Test",
                    ImageUrl = "TestImage.jpg",
                    Order = 0,
                    Price = 10,
                    Event = new EventDto()
                    {
                        Id = 1,
                        Name = "TestEvent"
                    }
                },
                new ProductDto()
                {
                    Id = 2,
                    Name = "Test2",
                    ImageUrl = "TestImage2.jpg",
                    Order = 1,
                    Price = 22,
                    Event = new EventDto()
                    {
                        Id = 1,
                        Name = "TestEvent"
                    }
                }
            });
            var controller = new CatalogController(productMock.Object, configurationMock.Object);

            // Act
            var result = controller.Shop(1, 5);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CatalogViewModel>(
                viewResult.ViewData.Model);

            Assert.Equal(2, model.Products.Products.Count());
            Assert.Equal(5, model.EventId);
            Assert.Equal(1, model.SectionId);
            Assert.Equal("TestImage2.jpg", model.Products.Products.ToList()[1].ImageUrl);
        }

    }
}
