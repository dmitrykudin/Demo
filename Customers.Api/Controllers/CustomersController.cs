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
    public class CustomersController : ApiController
    {
        ICustomerRepository customerRepository = new CustomerRepository();

        [HttpPost]
        [Route("api/customers")]
        public Customer Create([FromBody] CreateCustomerParameters customerParameters)
        {
            return customerRepository.CreateCustomer(
                customerParameters.Email,
                customerParameters.PasswordHash,
                customerParameters.FirstName,
                customerParameters.LastName,
                customerParameters.Age);
        }

        // GET: api/Customers/5
        [HttpGet]
        [Route("api/customers/{id}")]
        public CustomerForPurchaseController Get(Guid id)
        {
            CustomerForPurchaseController customer = Mapper.Map<Customer, CustomerForPurchaseController>(customerRepository.GetCustomer(id));
            return customer;
        }

        [HttpPost]
        [Route("api/customers/{id}")]
        public Customer Update(Guid id, [FromBody] CreateCustomerParameters newParameters)
        {
            //TODO
            return new Customer();
        }

        // DELETE: api/Customers/5
        [HttpDelete]
        [Route("api/customers/{id}")]
        public void Delete(Guid id)
        {
            //TODO
        }
    }
}
