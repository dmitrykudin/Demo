using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.DataLayer
{
    public interface IProductItemRepository
    {
        ProductItem CreateProductItem(Guid productId, Guid purchaseId, decimal price, decimal quantity, decimal sum);
        void DeleteProductItem(int id);
    }
}
