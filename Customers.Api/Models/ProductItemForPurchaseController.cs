using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customers.Api.Models
{
    public class ProductItemForPurchaseController
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Sum { get; set; }
        public Guid ProductId { get; set; }
        public Guid PurchaseId { get; set; }
        public virtual ProductForPurchaseController Product { get; set; }
    }
}