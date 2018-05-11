using OrderPrice.Model.DomainLayer.Entities;
using OrderPrice.Services.BusinessExceptions;
using OrderPrice.Services.ServiceLayer;
using System;
using System.Collections.Generic;

namespace OrderPrice.Services.DomainLayer
{
    public class OrderService : IOrderService
    {
        private readonly decimal VAT = 23;
        private readonly IProductService _productService;

        public OrderService(IProductService productService)
        {
            _productService = productService;
        }

        public decimal CalculateOrder(Order order)
        {
            decimal vat = Math.Round(order.Total * (VAT / 100), 2);
            return order.Total + vat;
        }

        /// <summary>df
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Order PlaceOrder(Dictionary<string, int> request)
        {
            var orderItems = new List<OrderItem>();
            foreach (var item in request)
            {
                var product = _productService.GetProduct(item.Key);

                // Check product existence
                if (product == null)
                {
                    throw new Exception($"Product Id '{item.Key}' does not exist");
                }

                // Check stock availability
                if (product.InStock < item.Value)
                {
                    throw new ProductOutOfStockException($"Product '{product.Id}' is out of stock. " +
                                                         $"Requested: {item.Value}. " +
                                                         $"In stock: {product.InStock}");
                }

                orderItems.Add(new OrderItem
                {
                    Product = product,
                    Quantity = item.Value
                });
            }

            return new Order
            {
                OrderItems = orderItems
            };
        }
    }
}