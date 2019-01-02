using AutoMapper;
using Customers.Api.Models;
using EntityDatabase.DataLayer;
using EntityDatabase.DataLayer.MSSQL;
using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Customers.Api.Controllers
{
    public class ProductClassesController : ApiController
    {
        public IProductClassRepository productClassRepository = new ProductClassRepository();
        
        [HttpGet]
        [Route("api/productclasses")]
        public List<ProductClassForProductClassController> GetAllProductClasses()
        {
            List<ProductClassForProductClassController> productClasses =
                Mapper.Map<List<ProductClass>, List<ProductClassForProductClassController>>(productClassRepository.GetAllProductClasses());
            return productClasses;
        }

        [HttpGet]
        [Route("api/productclasses/{id}")]
        public ProductClassForProductClassController GetProductClass(int id)
        {
            ProductClassForProductClassController productClass =
                Mapper.Map<ProductClass, ProductClassForProductClassController>(productClassRepository.GetProductClass(id));
            return productClass;
        }

    }
}
