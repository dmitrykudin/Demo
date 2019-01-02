using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityDatabase.DataLayer.MSSQL;
using EntityDatabase.EntityModels;

namespace DataLayer.MSSQL.Tests
{
    [TestClass]
    public class PurchaseRepositoryTest
    {
        [TestMethod]
        public void ShouldCreatePurchase()
        {
            string customerMail = "myemail@emails.com";
            Guid shopId = new Guid("DF058C1F-B253-E811-BFD6-001583C810FA");

            CustomerRepository customerRepo = new CustomerRepository();
            ShopRepository shopRepo = new ShopRepository();
            PurchaseRepository purchaseRepo = new PurchaseRepository();

            Shop shop = shopRepo.GetShop(shopId);
            Customer customer = customerRepo.GetCustomer(customerMail);
            Purchase purchase = purchaseRepo.CreatePurchase(customer.Id, shop.Id, DateTime.Now, new decimal(123.45));

            Assert.AreEqual(customer.Id, purchase.CustomerId);
            Assert.AreEqual(shopId, purchase.ShopId);
            Assert.AreEqual(new decimal(123.45), purchase.PurchaseSum);
        }

        [TestMethod]
        public void ShouldCreatePurchaseWithItems()
        {
            Guid customerId = new Guid("296BE888-B153-E811-BFD6-001583C810FA");
            Guid shopId = new Guid("DF058C1F-B253-E811-BFD6-001583C810FA");
            Guid product1Id = new Guid("D309B217-BE53-E811-BFD6-001583C810FA");
            Guid product2Id = new Guid("DD09B217-BE53-E811-BFD6-001583C810FA");
            Guid product3Id = new Guid("DE09B217-BE53-E811-BFD6-001583C810FA");
            Guid product4Id = new Guid("E809B217-BE53-E811-BFD6-001583C810FA");

            PurchaseRepository purchaseRepository = new PurchaseRepository();
            Purchase purchase = purchaseRepository.CreatePurchase(
                customerId,
                shopId,
                DateTime.Now,
                500);

            ProductItemRepository productItemRepository = new ProductItemRepository();
            ProductItem productItem1 = productItemRepository.CreateProductItem(
                    product1Id,
                    purchase.Id,
                    100,
                    1,
                    100);
            ProductItem productItem2 = productItemRepository.CreateProductItem(
                    product2Id,
                    purchase.Id,
                    100,
                    1,
                    100);
            ProductItem productItem3 = productItemRepository.CreateProductItem(
                    product3Id,
                    purchase.Id,
                    100,
                    1,
                    100);
            ProductItem productItem4 = productItemRepository.CreateProductItem(
                    product4Id,
                    purchase.Id,
                    100,
                    1,
                    100);

        }

        [TestMethod]
        public void ShouldGetPurchase()
        {
            Guid id = new Guid("ADFCF49D-CD53-E811-BFD6-001583C810FA");
            PurchaseRepository purchaseRepo = new PurchaseRepository();
            Purchase purchase = purchaseRepo.GetPurchase(id);

            //TODO Допилить
            Assert.AreEqual(id, purchase.Id);
        }
    }
}
