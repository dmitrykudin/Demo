using AutoMapper;
using Customers.Api.Models;
using EntityDatabase.DataLayer;
using EntityDatabase.DataLayer.MSSQL;
using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Customers.Api.Controllers
{
    public class PurchasesController : ApiController
    {
        public IShopRepository shopRepository = new ShopRepository();
        public ICustomerRepository customerRepository = new CustomerRepository();
        public IPurchaseRepository purchaseRepository = new PurchaseRepository();
        public IProductRepository productRepository = new ProductRepository();
        public IProductItemRepository productItemRepository = new ProductItemRepository();
        
        [HttpPost]
        [Route("api/purchases/{customerId}")]
        public PurchaseForPurchaseController CreatePurchase(Guid customerId, [FromBody] ReceiptModel receiptModel)
        {
            Shop shop = shopRepository.CreateShop(
                Convert.ToInt64(receiptModel.UserInn),
                receiptModel.User,
                receiptModel.RetailPlaceAddress);

            Customer customer = customerRepository.GetCustomer(customerId);

            Purchase purchase = purchaseRepository.CreatePurchase(
                customer.Id,
                shop.Id,
                Convert.ToDateTime(receiptModel.DateTime),
                receiptModel.TotalSum
                );

            foreach (var item in receiptModel.Items)
            {
                Product product = productRepository.CreateProduct(item.Name);
                ProductItem productItem = productItemRepository.CreateProductItem(
                    product.Id,
                    purchase.Id,
                    item.Price / 100,
                    item.Quantity,
                    item.Sum / 100);
            }
            PurchaseForPurchaseController purchaseInfo =
                Mapper.Map<Purchase, PurchaseForPurchaseController>(purchaseRepository.GetUserPurchase(purchase.Id, customerId));
            return purchaseInfo;
        }

        [HttpGet]
        [Route("api/purchases/{customerId}")]
        public List<PurchaseForPurchaseController> GetUserPurchases(Guid customerId)
        {
            List<PurchaseForPurchaseController> purchases = Mapper.Map<List<Purchase>, List<PurchaseForPurchaseController>>(purchaseRepository.GetUserPurchases(customerId));
            return purchases;
        }

        [HttpGet]
        [Route("api/purchases/{customerId}/{from}/{to}")]
        public List<PurchaseForPurchaseController> GetUserPurchases(Guid customerId, string from, string to)
        {
            DateTime dateFrom, dateTo;
            if (DateTime.TryParse(from, out dateFrom) && DateTime.TryParse(to, out dateTo))
            {
                List<PurchaseForPurchaseController> purchases = Mapper.Map<List<Purchase>, List<PurchaseForPurchaseController>>(purchaseRepository.GetUserPurchases(customerId, dateFrom, dateTo));
                return purchases;
            }
            else return null;
            
        }

        [HttpGet]
        [Route("api/purchases/{customerId}/{purchaseId}")]
        public PurchaseForPurchaseController GetUserPurchase(Guid customerId, Guid purchaseId)
        {
            PurchaseForPurchaseController purchase =
                Mapper.Map<Purchase, PurchaseForPurchaseController>(purchaseRepository.GetUserPurchase(purchaseId, customerId));
            return purchase;
        }

        [HttpDelete]
        [Route("api/purchases/{customerId}/{purchaseId}")]
        public void DeletePurchase(Guid id)
        {
            //TODO
        }
    }
}
