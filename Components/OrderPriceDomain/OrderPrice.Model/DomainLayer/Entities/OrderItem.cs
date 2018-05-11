namespace OrderPrice.Model.DomainLayer.Entities
{
    public class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Quantity * Product.Price;
    }
}