using System.Collections.Generic;
using System.Linq;

namespace OrderPrice.Model.DomainLayer.Entities
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        /// <summary>
        /// Order items 
        /// </summary>
        public List<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Total without VAT
        /// </summary>
        public decimal Total => OrderItems.Sum(o => o.Total);
    }
}