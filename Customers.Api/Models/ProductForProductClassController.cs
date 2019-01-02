using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customers.Api.Models
{
    public class ProductForProductClassController
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? Rating { get; set; }
        public int ClassId { get; set; }
    }
}