using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.DataLayer.MSSQL
{
    public class PurchaseRepository : IPurchaseRepository
    {
        public Purchase CreatePurchase(Guid customerId, Guid shopId, DateTime date, decimal purchaseSum)
        {
            using (var cc = new CustomersContext())
            {
                Purchase purchase = cc.Purchases.Add(new Purchase()
                {
                    CustomerId = customerId,
                    ShopId = shopId,
                    Date = date,
                    PurchaseSum = purchaseSum
                });
                cc.SaveChanges();
                return purchase;
            }
        }

        public Purchase GetPurchase(Guid id)
        {
            using (var cc = new CustomersContext())
            {
                Purchase purchase = cc.Purchases.Include(p => p.ProductItems.Select(i => i.Product)).FirstOrDefault(p => p.Id == id);
                return purchase;
            }
        }
        
        public Purchase GetUserPurchase(Guid purchaseId, Guid customerId)
        {
            using (var cc = new CustomersContext())
            {
                Purchase purchase = cc.Purchases.Include(p => p.ProductItems.Select(i => i.Product)).FirstOrDefault(p => p.Id == purchaseId && p.CustomerId == customerId);
                return purchase;
            }
        }

        public List<Purchase> GetAllPurchases()
        {
            using (var cc = new CustomersContext())
            {
                return cc.Purchases.Include(p => p.ProductItems).ToList();
            }
        }

        public List<Purchase> GetUserPurchases(Guid userId)
        {
            using (var cc = new CustomersContext())
            {
                List<Purchase> purchaseList = cc.Purchases.Where(p => p.CustomerId == userId).Include(p => p.ProductItems).ToList();
                foreach (var purchase in purchaseList)
                {
                    foreach (var productItem in purchase.ProductItems)
                    {
                        productItem.Product = cc.Products.FirstOrDefault(p => p.Id == productItem.ProductId);
                    }
                }
                return purchaseList;
            }
        }

        public List<Purchase> GetUserPurchases(Guid userId, DateTime from, DateTime to)
        {
            using (var cc = new CustomersContext())
            {
                List<Purchase> purchaseList = cc.Purchases.Where(p => p.CustomerId == userId && p.Date <= to && p.Date >= from).Include(p => p.ProductItems).ToList();
                foreach (var purchase in purchaseList)
                {
                    foreach (var productItem in purchase.ProductItems)
                    {
                        productItem.Product = cc.Products.FirstOrDefault(p => p.Id == productItem.ProductId);
                    }
                }
                return purchaseList;
            }
        }

    }
}
