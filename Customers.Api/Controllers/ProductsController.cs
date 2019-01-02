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
    public class ProductsController : ApiController
    {
        public IProductRepository productRepository = new ProductRepository();
        public IProductClassRepository productClassRepository = new ProductClassRepository();
        
        [HttpGet]
        [Route("api/products/{id}")]
        public ProductForProductController GetProduct(Guid id)
        {            
            ProductForProductController product = 
                Mapper.Map<Product, ProductForProductController>(productRepository.GetProductById(id));
            product.RelatedProducts = 
                Mapper.Map<List<Product>, List<ProductForProductController>>( productRepository.GetRelatedProducts(id));
            product.ClassName = productClassRepository.GetProductClass(product.ClassId).Name;
            return product;
        }

        [HttpGet]
        [Route("api/products/{id}/{from}/{to}")]
        public ProductForProductController GetProduct(Guid id, DateTime from, DateTime to)
        {
            ProductForProductController product =
                Mapper.Map<Product, ProductForProductController>(productRepository.GetProductById(id, from, to));
            product.RelatedProducts =
                Mapper.Map<List<Product>, List<ProductForProductController>>(productRepository.GetRelatedProducts(id));
            product.ClassName = productClassRepository.GetProductClass(product.ClassId).Name;
            return product;
        }

        [HttpGet]
        [Route("api/products/{id}/related")]
        public List<ProductForProductController> GetRelatedProducts(Guid id)
        {
            List<ProductForProductController> relatedProducts =
                Mapper.Map<List<Product>, List<ProductForProductController>>(productRepository.GetRelatedProducts(id));
            return relatedProducts;
        }

        [HttpPost]
        [Route("api/products/{productId}/{customerId}/{rating}")]
        public void SetRating(Guid productId, Guid customerId, decimal rating)
        {
            //TODO
        }

    }
}
