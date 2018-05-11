using System.Collections.Generic;
using OrderPrice.Model.DomainLayer.Entities;

namespace OrderPrice.Model.RepositoryInterfaces
{
    public interface IProductRepository
    {
        Product GetProduct(string id);
        IEnumerable<Product> GetAllProductsInStock();
    }
}