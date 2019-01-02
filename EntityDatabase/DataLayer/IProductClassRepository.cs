using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.DataLayer
{
    public interface IProductClassRepository
    {
        ProductClass CreateProductClass(string name, string shortName = null, int? parentId = null);

        ProductClass GetProductClass(int id);
        ProductClass GetProductClass(string name);
        int? GetProductClassId(string name);

        List<ProductClass> GetProductClassDescendants(int id);
        List<ProductClass> GetProductClassChildren(int id);
        ProductClass GetProductClassParent(int id); 

        void DeleteProductClass(int id);
        void DeleteProductClass(string name);

        List<ProductClass> GetAllProductClasses();
        List<ProductClass> GetProductClassesByTag(Tag tag);
    }
}
