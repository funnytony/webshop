using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShop.Domain.DTO.Product;
using WebShop.Infrastructure.Interfaces;
using WebShop.Interfaces;
using WebShop.Models;
using WebShop.Models.Cart;
using WebShop.Services;
using Xunit;

namespace WebShop.Tests
{
    public class CartServiceTests
    {
        [Fact]
        public void Cart_Class_ItemsCount_Returns_Correct_Quantity()
        {
            Cart cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem()
                    {
                        ProductId = 1,
                        Quantity = 1
                    },
                    new CartItem()
                    {
                        ProductId = 3,
                        Quantity = 3
                    }
                }
            };

            int result = cart.ItemsCount;

            Assert.Equal(4, result);
        }

        [Fact]
        public void CartViewModel_Returns_Correct_ItemsCount()
        {
            CartViewModel cartViewModel = new CartViewModel()
            {
                Items = new Dictionary<ProductViewModel, int>()
                {
                    {
                        new ProductViewModel()
                        {
                            Id = 1,
                            Name = "TestItem",
                            Price = 5
                        },
                        1
                    },
                    {
                        new ProductViewModel()
                        {
                            Id = 2,
                            Name = "TestItem2",
                            Price = 1
                        },
                        2
                    },

                }
            };

            int result = cartViewModel.ItemsCount;

            Assert.Equal(3, result);
        }

        [Fact]
        public void CartViewModel_Returns_Correct_PriceSum()
        {
            CartViewModel cartViewModel = new CartViewModel()
            {
                Items = new Dictionary<ProductViewModel, int>()
                {
                    {
                        new ProductViewModel()
                        {
                            Id = 1,
                            Name = "TestItem",
                            Price = 5
                        },
                        1
                    },
                    {
                        new ProductViewModel()
                        {
                            Id = 2,
                            Name = "TestItem2",
                            Price = 1
                        },
                        2
                    },

                }
            };

            decimal result = cartViewModel.PriceSum;

            Assert.Equal(7.0m, result);

        }

        [Fact]
        public void CartService_AddToCart_WorksCorrect()
        {
            Cart cart = new Cart()
            {
                Items = new List<CartItem>()
            };

            Mock<IProductData> productData = new Mock<IProductData>();
            Mock<ICartStore> cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            CartService cartService = new CartService(productData.Object, cartStore.Object);

            cartService.AddToCart(5);

            Assert.Equal(1, cart.ItemsCount);
            Assert.Equal(1, cart.Items.Count);
            Assert.Equal(5, cart.Items[0].ProductId);
        }

        [Fact]
        public void CartService_AddToCart_Increment_Quantity()
        {
            Cart cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem(){ProductId = 5, Quantity = 2}
                }
            };

            Mock<IProductData> productData = new Mock<IProductData>();
            Mock<ICartStore> cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            CartService cartService = new CartService(productData.Object, cartStore.Object);

            cartService.AddToCart(5);

            Assert.Equal(1, cart.Items.Count);
            Assert.Equal(3, cart.ItemsCount);
        }

        [Fact]
        public void CartService_RemoveFromCart_Removes_Correct_Item()
        {
            Cart cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem(){ProductId = 1,Quantity = 3},
                    new CartItem(){ProductId = 2, Quantity = 1}
                }
            };

            Mock<IProductData> productData = new Mock<IProductData>();
            Mock<ICartStore> cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            CartService cartService = new CartService(productData.Object, cartStore.Object);

            cartService.RemoveFromCart(1);

            Assert.Equal(1, cart.Items.Count);
            Assert.Equal(2, cart.Items[0].ProductId);
        }

        [Fact]
        public void CartService_RemoveAll_Clear_Cart()
        {
            var cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem(){ProductId = 1,Quantity = 3},
                    new CartItem(){ProductId = 2, Quantity = 1}
                }
            };

            var productData = new Mock<IProductData>();
            var cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            cartService.RemoveAll();

            Assert.Equal(0, cart.Items.Count);
        }

        [Fact]
        public void CartService_Decrement_Correct()
        {
            var cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem(){ProductId = 1,Quantity = 3},
                    new CartItem(){ProductId = 2, Quantity = 1}
                }
            };

            var productData = new Mock<IProductData>();
            var cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            cartService.DecrementFromCart(1);

            Assert.Equal(3, cart.ItemsCount);
            Assert.Equal(2, cart.Items.Count);
            Assert.Equal(1, cart.Items[0].ProductId);
        }

        [Fact]
        public void CartService_Remove_Item_When_Decrement()
        {
            var cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem(){ProductId = 1,Quantity = 3},
                    new CartItem(){ProductId = 2, Quantity = 1}
                }
            };

            var productData = new Mock<IProductData>();
            var cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            cartService.DecrementFromCart(2);

            Assert.Equal(3, cart.ItemsCount);
            Assert.Equal(1, cart.Items.Count);
        }

        [Fact]
        public void CartService_TransformCart_WorksCorrect()
        {
            var cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem(){ProductId = 1, Quantity = 4}
                }
            };
            var products = new List<ProductDto>()
            {
                new ProductDto()
                {
                    Id = 1,
                    ImageUrl = "",
                    Name = "Test",
                    Order = 0,
                    Price = 1,
                }
            };

            var productData = new Mock<IProductData>();
            productData.Setup(c => c.GetProducts(It.IsAny<ProductFilter>())).Returns(products);
            var cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            var result = cartService.TransformCart();

            Assert.Equal(4, result.ItemsCount);
            Assert.Equal(1, result.Items.First().Key.Price);

        }





    }
}
