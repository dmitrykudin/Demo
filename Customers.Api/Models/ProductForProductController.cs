using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customers.Api.Models
{
    public class ProductForProductController
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? Rating { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public List<ProductItemForProductController> ProductItems { get; set; }
        public List<ProductForProductController> RelatedProducts { get; set; }

        public int ProductItemsNum
        {
            get
            {
                return ProductItems.Count;
            }
        }

        public decimal TotalMoneySpent
        {
            get
            {
                decimal sum = 0.0M;
                foreach (var item in ProductItems)
                {
                    sum += item.Sum;
                }
                return sum;
            }
        }
    }
}