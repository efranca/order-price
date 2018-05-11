using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderPrice.Model.DomainLayer.Entities;
using OrderPrice.Services.BusinessExceptions;
using OrderPrice.Services.DomainLayer;
using OrderPrice.Services.ServiceLayer;
using System;
using System.Collections.Generic;

namespace OrderPrice.Services.Tests.DomainLayer
{
    [TestClass]
    public class OrderServiceTests
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderServiceTests()
        {
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(p => p.GetProduct("P1")).Returns(new Product
            {
                Id = "P1",
                Price = 22,
                InStock = 300,
            });

            productServiceMock.Setup(p => p.GetProduct("P2")).Returns(new Product
            {
                Id = "P2",
                Price = (decimal) 550.87,
                InStock = 5,
            });

            _productService = productServiceMock.Object;
            _orderService = new OrderService(_productService);
        }

        [TestMethod]
        public void CalculateOrderOkTest()
        {
            const decimal expected = (decimal) 3469.03;

            var order = new Order()
            {
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem { Product = _productService.GetProduct("P1"), Quantity = 3},
                    new OrderItem { Product = _productService.GetProduct("P2"), Quantity = 5}
                }
            };

            var actual = _orderService.CalculateOrder(order);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ProductOutOfStockException))]
        public void OrderOutOufStockTest()
        {
            var clientOrder = new Dictionary<string, int> {{"P2", 100}};
            _orderService.PlaceOrder(clientOrder);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void OrderProductNotExistentTest()
        {
            var clientOrder = new Dictionary<string, int> { { "P3", 100 } };
            _orderService.PlaceOrder(clientOrder);
        }
    }
}