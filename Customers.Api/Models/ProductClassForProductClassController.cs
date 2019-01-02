using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customers.Api.Models
{
    public class ProductClassForProductClassController
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<ProductForProductClassController> Products { get; set; }
        public List<ProductClassForProductClassController> ChildrenClasses { get; set; }
    }
}