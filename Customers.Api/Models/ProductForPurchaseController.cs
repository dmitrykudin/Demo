﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customers.Api.Models
{
    public class ProductForPurchaseController
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? Rating { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
    }
}