using OrderPrice.Model.DomainLayer.Entities;
using System.Collections.Generic;

namespace OrderPrice.Services.ServiceLayer
{
    public interface IOrderService
    {
        /// <summary>
        /// Interpret a dictionary composed of ProductId and Quantity
        /// into an Order object
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Order PlaceOrder(Dictionary<string, int> request);
        /// <summary>
        /// Calculate order amount with VAT
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        decimal CalculateOrder(Order order);
    }
}