using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityDatabase.DataLayer.MSSQL;
using EntityDatabase.EntityModels;

namespace DataLayer.MSSQL.Tests
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        [TestMethod]
        public void ShouldCreateCustomer()
        {
            CustomerRepository repo = new CustomerRepository();
            string email = "myemail@emails.com";
            string passwordHash = "thisispasswordhashbelieveme";
            string firstName = "me";
            string lastName = "actualyMe";
            int age = 120;
            Customer trueCustomer = repo.CreateCustomer(email, passwordHash, firstName, lastName, age);
            Customer fakeCustomer = new Customer() { Email = email, PasswordHash = passwordHash, FirstName = firstName, LastName = lastName, Age = age };

            Assert.AreEqual(fakeCustomer.Email, trueCustomer.Email);
            Assert.AreEqual(fakeCustomer.PasswordHash, trueCustomer.PasswordHash);
            Assert.AreEqual(fakeCustomer.FirstName, trueCustomer.FirstName);
            Assert.AreEqual(fakeCustomer.LastName, trueCustomer.LastName);
            Assert.AreEqual(fakeCustomer.Age, trueCustomer.Age);
        }

        [TestMethod]
        public void ShouldGetCustomer()
        {
            string email = "myemail@emails.com";
            CustomerRepository repo = new CustomerRepository();
            Customer customer = repo.GetCustomer(email);

            Assert.AreEqual(email, customer.Email);
        }

        [TestMethod]
        public void ShouldDeleteCustomer()
        {
            string email = "myemail@emails.com";
            CustomerRepository repo = new CustomerRepository();
            repo.DeleteCustomer(email);
            
            Customer trueCustomer = repo.GetCustomer(email);

            Assert.AreEqual(null, trueCustomer);
        }

        [TestMethod]
        public void ShouldUpdateCustomer()
        {
            string email = "myemail@emails.com";
            string email1 = "myemail1@emails.com";
            string passwordHash = "thisispasswordhashbelieveme";
            string firstName = "me";
            string lastName = "actualyMe";
            int age = 120;

            CustomerRepository repo = new CustomerRepository();
            Guid id = repo.GetCustomerId(email);

            Customer customer = repo.UpdateCustomer(id, true);
            Assert.AreEqual(null, customer.FirstName);

            customer = repo.UpdateCustomer(id, false);
            Assert.AreEqual(passwordHash, customer.PasswordHash);

            customer = repo.UpdateCustomer(id, true, email1);
            Assert.AreEqual(email1, customer.Email);
        }
    }
}
