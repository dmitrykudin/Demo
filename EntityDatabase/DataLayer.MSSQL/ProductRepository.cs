using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using EntityDatabase.EntityModels;
using WordsMatching;

namespace EntityDatabase.DataLayer.MSSQL
{
    public class ProductRepository : IProductRepository
    {
        private const int NonClassifiedProductsClassId = 1;

        public Product CreateProduct(string name, int classId)
        {
            using (var cc = new CustomersContext())
            {
                Product product = cc.Products.FirstOrDefault(p => p.Name == name && p.ClassId == classId);
                if (product == null)
                {
                    ProductClass productClass = cc.ProductClasses.FirstOrDefault(c => c.Id == classId);
                    if (productClass != null)
                    {
                        product = cc.Products.Add(new Product()
                        {
                            Name = name,
                            ClassId = productClass.Id
                        });
                    }
                    else
                    {
                        product = cc.Products.Add(new Product()
                        {
                            Name = name,
                            ClassId = NonClassifiedProductsClassId
                        });
                    }
                    cc.SaveChanges();
                }                
                return product;
            }
        }

        public Product CreateProduct(string name)
        {
            using (var cc = new CustomersContext())
            {
                List<Product> products = cc.Products.ToList();
                Dictionary<Product, float> scores = new Dictionary<Product, float>();
                foreach (var pr in products)
                {                    
                    MatchsMaker match = new MatchsMaker(pr.Name, name);
                    if (pr.Name == name || match.Score >= 0.85)
                    {
                        scores.Add(pr, match.Score);
                    }
                }
                Product product = null;

                if (scores.Count > 0)
                {
                    var scoresList = scores.ToList();
                    scoresList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
                    product = scoresList.ElementAt(0).Key;
                }                
                
                List<ProductClass> productClasses = cc.ProductClasses.Include(pc => pc.ClassTags.Select(ct => ct.Tag)).ToList();
                float maxSimilarity = 0.0F;
                ProductClass specialOne = new ProductClass();
                foreach (var cl in productClasses)
                {
                    string tagStr = "";
                    try
                    {
                        tagStr = string.Join(" ", cl.ClassTags.Select(ct => ct.Tag.TagName));
                    }
                    catch(Exception e)
                    {
                        continue;
                    }
                    
                    MatchsMaker match = new MatchsMaker(name, tagStr);
                    if (maxSimilarity < match.Score)
                    {
                        maxSimilarity = match.Score;
                        specialOne = cl;
                    }
                }

                if (product == null)
                {
                    if (specialOne != null)
                    {
                        product = cc.Products.Add(new Product()
                        {
                            Name = name,
                            ClassId = specialOne.Id
                        });
                    }
                    else
                    {
                        product = cc.Products.Add(new Product()
                        {
                            Name = name,
                            ClassId = NonClassifiedProductsClassId
                        });
                    }                    
                }
                else
                {
                    product.ClassId = specialOne != null ? specialOne.Id : NonClassifiedProductsClassId;

                }
                cc.SaveChanges();
                return product;
            }
        }

        public void DeleteProductById(Guid id)
        {
            using (var cc = new CustomersContext())
            {
                Product product = cc.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    cc.Products.Remove(product);
                    cc.SaveChanges();
                }
            }
        }

        public void DeleteProductByName(string name)
        {
            using (var cc = new CustomersContext())
            {
                Product product = cc.Products.FirstOrDefault(p => p.Name == name);
                if (product != null)
                {
                    cc.Products.Remove(product);
                    cc.SaveChanges();
                }
            }
        }

        public List<Product> GetAllProducts()
        {
            using (var cc = new CustomersContext())
            {
                return cc.Products.ToList();
            }
        }

        public Product GetProductById(Guid id)
        {
            using (var cc = new CustomersContext())
            {
                Product product = cc.Products
                    .Include(p => p.ProductItems.Select(i => i.Purchase))
                    .FirstOrDefault(p => p.Id == id);
                return product;
            }
        }

        public Product GetProductById(Guid id, DateTime from, DateTime to)
        {
            using (var cc = new CustomersContext())
            {
                Product product = cc.Products
                    .Include(p => p.ProductItems.Select(i => i.Purchase))
                    .FirstOrDefault(pr => pr.Id == id);
                List<ProductItem> productItems = product.ProductItems.Where(i => i.Purchase.Date >= from && i.Purchase.Date <= to).ToList();
                product.ProductItems = productItems;
                return product;
            }
        }

        public Product GetProductByName(string name)
        {
            using (var cc = new CustomersContext())
            {
                Product product = cc.Products.FirstOrDefault(p => p.Name == name);
                return product;
            }
        }

        public List<Product> GetRelatedProducts(Guid id)
        {
            IAssociationRuleRepository associationRuleRepository = new AssociationRuleRepository();
            List<AssociationRule> associationRules = associationRuleRepository.GetRulesByProduct(id);
            Dictionary<Product, decimal> tempDict = new Dictionary<Product, decimal>();

            foreach (var rule in associationRules)
            {
                foreach (var res in rule.RuleResults)
                {
                    if (tempDict.ContainsKey(res.Product))
                    {
                        if (tempDict[res.Product] < rule.Confidence)
                        {
                            tempDict[res.Product] = rule.Confidence;
                        }
                    }
                    else
                    {
                        tempDict.Add(res.Product, rule.Confidence);
                    }                    
                }
            }

            List<KeyValuePair<Product, decimal>> tempList = tempDict.ToList();
            tempList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            List<Product> relatedProducts = new List<Product>();
            foreach (var item in tempList)
            {
                relatedProducts.Add(item.Key);
            }

            if (relatedProducts.Count >= 5)
                return relatedProducts.Take(5).ToList();
            else
                return relatedProducts;

        }
    }
}
