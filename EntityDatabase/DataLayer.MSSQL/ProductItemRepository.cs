using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDatabase.EntityModels;

namespace EntityDatabase.DataLayer.MSSQL
{
    public class ProductItemRepository : IProductItemRepository
    {
        public ProductItem CreateProductItem(Guid productId, Guid purchaseId, decimal price, decimal quantity, decimal sum)
        {
            using (var cc = new CustomersContext())
            {
                ProductItem productItem = cc.ProductItems.Add(new ProductItem()
                {
                    ProductId = productId,
                    PurchaseId = purchaseId,
                    Price = price,
                    Quantity = quantity,
                    Sum = sum
                });
                cc.SaveChanges();
                return productItem;
            }
        }

        // TODO Лучше добавлять пачкой

        public void DeleteProductItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
