using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.DataLayer
{
    public interface IProductRatingRepository
    {
        ProductRating CreateProductRating(Guid customerId, Guid productId, decimal rating);
        void DeleteProductRating(int ratingId);
        ProductRating GetProductRating(int ratingId);
        ProductRating GetProductRating(Guid customerId, Guid productId);
    }
}
