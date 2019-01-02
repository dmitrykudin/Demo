using EntityDatabase.DataLayer.MSSQL;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityDatabase.EntityModels;

namespace DataLayer.MSSQL.Tests
{
    [TestClass]
    public class ProductRepositoryTest
    {
        // TODO Реализовать
        [TestMethod]
        public void ShouldCreateProduct()
        {
            ProductRepository productRepository = new ProductRepository();
            Product product = productRepository.CreateProduct("Пиво светлое нефильтрованное");

            Assert.AreEqual("Пиво светлое нефильтрованное", product.Name);
        }

        [TestMethod]
        public void ShouldDeleteProductById()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldDeleteProductByName()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldGetProductById()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldGetProductByName()
        {
            throw new NotImplementedException();
        }
    }
}
