using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.DataLayer
{
    public interface IProductRepository
    {
        Product CreateProduct(string name);
        Product CreateProduct(string name, int classId);

        Product GetProductById(Guid id);
        Product GetProductById(Guid id, DateTime from, DateTime to);
        Product GetProductByName(string name);

        void DeleteProductById(Guid id);
        void DeleteProductByName(string name);

        List<Product> GetAllProducts();
        List<Product> GetRelatedProducts(Guid id);
    }
}
