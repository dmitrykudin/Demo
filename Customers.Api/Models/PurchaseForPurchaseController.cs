using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customers.Api.Models
{
    public class PurchaseForPurchaseController
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid ShopId { get; set; }

        public DateTime Date { get; set; }

        public decimal PurchaseSum { get; set; }

        public CustomerForPurchaseController Customer { get; set; }

        public ShopForPurchaseController Shop { get; set; }

        public List<ProductItemForPurchaseController> ProductItems { get; set; }
    }
}