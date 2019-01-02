using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using EntityDatabase.EntityModels;

namespace EntityDatabase.DataLayer.MSSQL
{
    public class ProductRatingRepository : IProductRatingRepository
    {
        public ProductRating CreateProductRating(Guid customerId, Guid productId, decimal rating)
        {
            using (var cc = new CustomersContext())
            {
                ProductRating productRating = cc.ProductRatings.FirstOrDefault(r => r.CustomerId == customerId && r.ProductId == productId);
                if (productRating == null)
                {
                    productRating = cc.ProductRatings.Add(new ProductRating()
                    {
                        CustomerId = customerId,
                        ProductId = productId,
                        Rating = rating
                    });
                }
                Product product = cc.Products.Include(p => p.ProductRatings).FirstOrDefault(p => p.Id == productId);
                product.Rating = product.ProductRatings.Average(r => r.Rating);
                cc.SaveChanges();
                return productRating;
            }
        }

        public void DeleteProductRating(int ratingId)
        {
            //TODO
            throw new NotImplementedException();
        }

        public ProductRating GetProductRating(int ratingId)
        {
            //TODO
            throw new NotImplementedException();
        }

        public ProductRating GetProductRating(Guid customerId, Guid productId)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
