using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderPrice.Infrastructure.InfrastructureLayer.Repositories;
using OrderPrice.Model.RepositoryInterfaces;
using System;
using System.IO;
using System.Linq;

namespace OrderPrice.Infrastructure.Tests.InfrastructureLayer.Repositories
{
    [TestClass]
    public class ProductsRepositoryTests
    {
        [DataTestMethod]
        [DataRow("Catalog.txt")]
        public void CheckExistentCatalogTest(string catalogPath)
        {
            const int expected = 3;
            IProductRepository repo = new ProductsRepository(catalogPath);
            var products = repo.GetAllProductsInStock();

            Assert.AreEqual(expected, products.Count());
        }
 
        [DataTestMethod]
        [DataRow("NonExistentCatalog.txt")]
        [ExpectedException(typeof(FileNotFoundException))]
        public void CheckNonExistentCatalogTest(string catalogPath)
        {
            var repo = new ProductsRepository(catalogPath);
            var products = repo.GetAllProductsInStock().ToList();
        }

        [DataTestMethod]
        [DataRow("Catalog2_ColumnsDoesNotMatch.txt")]
        [ExpectedException(typeof(Exception))]
        public void CatalogNumberOfColumnsDoesNotMatchTest(string catalogPath)
        {
            var repo = new ProductsRepository(catalogPath);
            var products = repo.GetAllProductsInStock().ToList();
        }

        [DataTestMethod]
        [DataRow("Catalog3_Empty.txt")]
        public void CatalogIsEmptyTest(string catalogPath)
        {
            var repo = new ProductsRepository(catalogPath);
            var products = repo.GetAllProductsInStock();

            Assert.IsFalse(products.Any());
        }

        [DataTestMethod]
        [DataRow("Catalog4_CorruptFile.txt")]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CatalogIsCorruptTest(string catalogPath)
        {
            var repo = new ProductsRepository(catalogPath);
            var products = repo.GetAllProductsInStock();

            Assert.IsFalse(products.Any());
        }
    }
}