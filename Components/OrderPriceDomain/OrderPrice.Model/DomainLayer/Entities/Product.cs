namespace OrderPrice.Model.DomainLayer.Entities
{
    public class Product
    {
        /// <summary>
        /// Product Identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Quantity of Items available
        /// </summary>
        public int InStock { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        public decimal Price { get; set; }
    }
}