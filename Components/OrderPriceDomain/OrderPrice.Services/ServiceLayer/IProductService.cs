using OrderPrice.Model.DomainLayer.Entities;

namespace OrderPrice.Services.ServiceLayer
{
    public interface IProductService
    {
        Product GetProduct(string id);
    }
}