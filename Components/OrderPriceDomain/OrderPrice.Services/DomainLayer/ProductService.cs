using OrderPrice.Infrastructure.InfrastructureLayer.Repositories;
using OrderPrice.Model.DomainLayer.Entities;
using OrderPrice.Model.RepositoryInterfaces;
using OrderPrice.Services.ServiceLayer;

namespace OrderPrice.Services.DomainLayer
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(string catalog)
        {
            _productRepository = new ProductsRepository(catalog);
        }

        public Product GetProduct(string id)
        {
            return _productRepository.GetProduct(id);
        }
    }
}