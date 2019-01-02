using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customers.Api.Models
{
    public class ShopForPurchaseController
    {
        public Guid Id { get; set; }
        public long Inn { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}