using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityDatabase.DataLayer.MSSQL;
using EntityDatabase.EntityModels;

namespace DataLayer.MSSQL.Tests
{
    [TestClass]
    public class ShopRepositoryTest
    {
        [TestMethod]
        public void ShouldCreateShop()
        {
            long inn = 12345678;
            string name = "Lenta";
            string address = "Komendantskiy pr-t 53";

            ShopRepository repo = new ShopRepository();
            Shop shop = repo.CreateShop(inn, name, address);

            Assert.AreEqual(inn, shop.Inn);
            Assert.AreEqual(name, shop.Name);
            Assert.AreEqual(address, shop.Address);
        }

        [TestMethod]
        public void ShouldGetShop()
        {
            Guid id = new Guid("1B990E1C-3D52-E811-BFD3-001583C810FA");
            ShopRepository repo = new ShopRepository();
            Shop shop = repo.GetShop(id);

            Assert.AreEqual(id, shop.Id);
        }
    }
}
