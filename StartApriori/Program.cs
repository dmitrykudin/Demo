using EntityDatabase.AprioriAlgorithm;
using EntityDatabase.DataLayer.MSSQL;
using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartApriori
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductRepository productRepository = new ProductRepository();
            PurchaseRepository purchaseRepository = new PurchaseRepository();
            AssociationRuleRepository associationRuleRepository = new AssociationRuleRepository();
            List<Product> products = productRepository.GetAllProducts();
            List<Purchase> purchases = purchaseRepository.GetAllPurchases();
            Apriori apriori = new Apriori();
            List<AssociationRule> associatiationRules = apriori.ProccessApriori(0.14, 0.1, products, purchases);
        }
    }
}
