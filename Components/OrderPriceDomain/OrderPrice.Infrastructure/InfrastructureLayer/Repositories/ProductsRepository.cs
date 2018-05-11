using OrderPrice.Model.DomainLayer.Entities;
using OrderPrice.Model.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrderPrice.Infrastructure.InfrastructureLayer.Repositories
{
    public class ProductsRepository : IProductRepository
    {
        private const char Delimiter = ',';
        private readonly IEnumerable<Product> _products;

        public ProductsRepository(string catalogPath)
        {
            _products = ReadDataSource(catalogPath);
        }

        public IEnumerable<Product> GetAllProductsInStock()
        {
            return _products;
        }

        public Product GetProduct(string id)
        {
            return _products.FirstOrDefault(o => o.Id == id);
        }
        
        private static IEnumerable<Product> ReadDataSource(string catalogPath)
        {
            int rowNumber = 1;
            int columnCount = 0;

            // Read the file and display it line by line.  
            using (var file = new StreamReader(catalogPath))
            {
                string row;
                while ((row = file.ReadLine()) != null)
                {
                    var rowColumns = row.Split(Delimiter);

                    if (rowNumber == 1)
                        columnCount = rowColumns.Length;

                    if (rowColumns.Length != columnCount)
                    {
                        throw new Exception($"Column count does not match at row '{rowNumber}'");
                    }

                    var productId = rowColumns[0].Trim();
                    var productsInStock = rowColumns[1].Trim();
                    var productPrice = rowColumns[2].Trim();

                    yield return new Product
                    {
                        Id = productId,
                        InStock = int.Parse(productsInStock),
                        Price = decimal.Parse(productPrice)
                    };

                    rowNumber++;
                }
            }
        }
    }
}