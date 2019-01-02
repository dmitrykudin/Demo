using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDatabase.EntityModels;
using System.Data.SqlClient;
using System.Data.Entity;

namespace EntityDatabase.DataLayer.MSSQL
{
    public class ProductClassRepository : IProductClassRepository
    {
        private const int NonClassifiedProductsClassId = 1;

        public ProductClass CreateProductClass(string name, string shortName = null, int? parentId = null)
        {
            if (name != null)
            {
                using (var cc = new CustomersContext())
                {
                    ProductClass productClass = new ProductClass()
                    {
                        Name = name,
                        ShortName = shortName
                    };

                    if (parentId != null)
                    {
                        ProductClass parentClass = cc.ProductClasses.FirstOrDefault(c => c.Id == parentId);
                        if (parentClass != null)
                        {
                            productClass.ParentId = parentClass.Id;
                        }
                    }
                    productClass = cc.ProductClasses.Add(productClass);
                    cc.SaveChanges();
                    return productClass;
                }
            }
            return null;
        }

        public void DeleteProductClass(int id)
        {
            using (var cc = new CustomersContext())
            {
                ProductClass productClass = cc.ProductClasses.FirstOrDefault(c => c.Id == id);
                if (productClass != null)
                {
                    // Просто при удалении класса сбрасываем ссылку на него у всех его потомков. Возможно, в будущем будем переносить всех в раздел "Неклассифицированное"
                    List<ProductClass> descendants = cc.ProductClasses.Where(c => c.ParentId == productClass.Id).ToList();
                    foreach (var item in descendants)
                    {
                        //item.ParentId = NonClassifiedProductsClassId;
                        item.ParentId = null;
                    }
                    cc.ProductClasses.Remove(productClass);
                    cc.SaveChanges();
                }                
            }
        }

        public void DeleteProductClass(string name)
        {
            using (var cc = new CustomersContext())
            {
                ProductClass productClass = cc.ProductClasses.FirstOrDefault(c => c.Name == name);
                if (productClass != null)
                {
                    // Просто при удалении класса сбрасываем ссылку на него у всех его потомков. Возможно, в будущем будем переносить всех в раздел "Неклассифицированное"
                    List<ProductClass> descendants = cc.ProductClasses.Where(c => c.ParentId == productClass.Id).ToList();
                    foreach (var item in descendants)
                    {
                        //item.ParentId = NonClassifiedProductsClassId;
                        item.ParentId = null;
                    }
                    cc.ProductClasses.Remove(productClass);
                    cc.SaveChanges();
                }                
            }
        }

        public ProductClass GetProductClass(int id)
        {
            using (var cc = new CustomersContext())
            {
                ProductClass productClass = cc.ProductClasses.Include(c => c.Products).FirstOrDefault(c => c.Id == id);
                productClass.ChildenClasses = GetProductClassDescendants(id);
                return productClass;
            }
        }

        public ProductClass GetProductClass(string name)
        {
            using (var cc = new CustomersContext())
            {
                ProductClass productClass = cc.ProductClasses.FirstOrDefault(c => c.Name == name);
                return productClass;
            }
        }

        public int? GetProductClassId(string name)
        {
            ProductClass productClass = GetProductClass(name);
            if (productClass != null)
            {
                return productClass.Id;
            }
            return null;
        }

        public List<ProductClass> GetProductClassDescendants(int id)
        {
            using (var cc = new CustomersContext())
            {
                var paramId = new SqlParameter
                {
                    ParameterName = "@ClassId",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = id
                };
                var sqlString = "EXEC GetAllDescendants @ClassId";
                List<ProductClass> descendants = cc.ProductClasses.SqlQuery(sqlString, paramId).ToList();
                descendants.Remove(descendants.ElementAt(0));
                return descendants;
            }
        }

        public List<ProductClass> GetProductClassChildren(int id)
        {
            using (var cc = new CustomersContext())
            {
                List<ProductClass> children = cc.ProductClasses.FirstOrDefault(c => c.Id == id).ChildenClasses;
                return children;
            }
        }

        public ProductClass GetProductClassParent(int id)
        {
            using (var cc = new CustomersContext())
            {
                ProductClass parent = cc.ProductClasses.FirstOrDefault(c => c.Id == id).ParentClass;
                return parent;
            }
        }

        public List<ProductClass> GetAllProductClasses()
        {
            using (var cc = new CustomersContext())
            {
                List<ProductClass> productClasses = cc.ProductClasses.ToList();
                return productClasses;
            }
        }

        public List<ProductClass> GetProductClassesByTag(Tag tag)
        {
            List<ProductClass> productClasses = new List<ProductClass>();
            using (var cc = new CustomersContext())
            {
                List<ClassTag> classTagMatches = cc.ClassTags.Where(ct => ct.TagId == tag.Id).ToList();
                foreach (var classTag in classTagMatches)
                {
                    List<ProductClass> temp = cc.ProductClasses.Where(c => c.Id == classTag.ClassId).Include(c => c.ClassTags).ToList();
                    foreach (var cl in temp)
                    {
                        foreach (var ct in cl.ClassTags)
                        {
                            ct.Tag = cc.Tags.FirstOrDefault(t => t.Id == ct.TagId);
                        }
                    }
                    productClasses.AddRange(temp);
                }
            }
            return productClasses;
        }
    }
}
