using AutoMapper;
using Customers.Api.Models;
using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customers.Api.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ProductClass, ProductClassForProductClassController>();
                cfg.CreateMap<Product, ProductForProductController>();
                cfg.CreateMap<Purchase, PurchaseForPurchaseController>();
                cfg.CreateMap<Customer, CustomerForPurchaseController>();
            });
        }        
    }
}