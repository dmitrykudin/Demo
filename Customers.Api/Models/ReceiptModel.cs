using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customers.Api.Models
{
    public class ReceiptModel
    {
        public string User { get; set; }
        public string UserInn { get; set; }
        public string RetailPlaceAddress { get; set; }
        public string DateTime { get; set; }
        public decimal TotalSum { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public decimal Sum { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}