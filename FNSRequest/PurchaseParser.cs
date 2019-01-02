using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EntityDatabase.EntityModels;
using EntityDatabase.DataLayer.MSSQL;

namespace FNSRequest
{
    public class PurchaseParser
    {
        private const int NonClassifiedProductsClassId = 1;

        private JObject _jsonData;
        public Purchase Purchase { get; set; }

        public PurchaseParser(JObject inputJson)
        {
            _jsonData = inputJson;
        }

        //retailPlaceAddress - строка с адресом магазина
        public void ParseJsonData(Guid customerId)
        {
            ShopRepository shopRepository = new ShopRepository();
            CustomerRepository customerRepository = new CustomerRepository();
            PurchaseRepository purchaseRepository = new PurchaseRepository();
            ProductRepository productRepository = new ProductRepository();
            ProductItemRepository productItemRepository = new ProductItemRepository();            
            
            Shop shop = shopRepository.CreateShop(
                (long)_jsonData["document"]["receipt"]["userInn"],
                (string)_jsonData["document"]["receipt"]["user"], 
                (string)_jsonData["document"]["receipt"]["retailPlaceAddress"]);

            Customer customer = customerRepository.GetCustomer(customerId);

            Purchase purchase = purchaseRepository.CreatePurchase(
                customer.Id,
                shop.Id,
                (DateTime)_jsonData["document"]["receipt"]["dateTime"],
                (decimal)_jsonData["document"]["receipt"]["totalSum"] / 100);

            //purchase.CustomerId = customer.Id;
            //purchase.Customer = customer;
            
            //purchase.ShopId = shop.Id;
            //purchase.Shop = shop;

            //purchase.Date = (DateTime) _jsonData["document"]["receipt"]["dateTime"];
            //purchase.PurchaseSum = (decimal)_jsonData["document"]["receipt"]["totalSum"] / 100;

            JArray items = (JArray)_jsonData["document"]["receipt"]["items"];
            foreach (var item in items)
            {
                Product product = productRepository.CreateProduct((string)item["name"]);
                ProductItem productItem = productItemRepository.CreateProductItem(
                    product.Id,
                    purchase.Id,
                    (decimal)item["price"] / 100,
                    (decimal)item["quantity"],
                    (decimal)item["sum"] / 100);
                //ProductItem productItem = new ProductItem();                
                //productItem.Price = (decimal) item["price"] / 100;
                //productItem.Quantity = (decimal) item["quantity"];
                //productItem.Sum = (decimal) item["sum"] / 100;
                //productItem.ProductId = product.Id;
                //productItem.Product = product;
                //productItem.PurchaseId = purchase.Id;
                //productItem.Purchase = purchase;                
            }
            Console.WriteLine("Parsed and added to database.");
        }
    }
}
