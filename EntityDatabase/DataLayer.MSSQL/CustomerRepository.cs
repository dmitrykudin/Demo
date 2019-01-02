using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDatabase.EntityModels;

namespace EntityDatabase.DataLayer.MSSQL
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer CreateCustomer(string email, string passwordHash, string firstName = null, string lastName = null, int? age = null)
        {
            if (email != null && passwordHash != null)
            {
                using (var cc = new CustomersContext())
                {
                    if (cc.Customers.FirstOrDefault(c => c.Email == email) != null)
                        return null;
                    cc.Customers.Add(new Customer()
                    {
                        Email = email,
                        PasswordHash = passwordHash,
                        FirstName = firstName,
                        LastName = lastName,
                        Age = age
                    });
                    cc.SaveChanges();
                    return GetCustomer(email);
                }
            }
            return null;
        }

        public void DeleteCustomer(Customer customer)
        {
            using (var cc = new CustomersContext())
            {
                customer = cc.Customers.Find(customer);
                cc.Customers.Remove(customer);
                cc.SaveChanges();
            }
        }

        public void DeleteCustomer(Guid id)
        {
            using (var cc = new CustomersContext())
            {
                Customer customer = cc.Customers.FirstOrDefault(c => c.Id == id);
                if (customer != null)
                {
                    cc.Customers.Remove(customer);
                    cc.SaveChanges();
                }
            }                        
        }

        public void DeleteCustomer(string email)
        {
            using (var cc = new CustomersContext())
            {
                Customer customer = cc.Customers.FirstOrDefault(c => c.Email == email);
                if (customer != null)
                {
                    cc.Customers.Remove(customer);
                    cc.SaveChanges();
                }
            }
        }

        public Customer GetCustomer(string email)
        {
            return GetCustomer(GetCustomerId(email));
        }

        public Customer GetCustomer(Guid id)
        {            
            if (id != null && id != Guid.Empty)
            {
                using (var cc = new CustomersContext())
                {
                    return cc.Customers.FirstOrDefault(c => c.Id == id);
                }
            }
            return null;
        }

        public Guid GetCustomerId(string email)
        {
            if (email != null)
            {
                using (var cc = new CustomersContext())
                {
                    Customer customer = cc.Customers.FirstOrDefault(c => c.Email == email);
                    if (customer != null)
                    {
                        return customer.Id;
                    }
                    return Guid.Empty;
                }
            }
            return Guid.Empty;
        }

        public Customer UpdateCustomer(Guid id, bool updateNulls, Customer newCustomer)
        {
            if (newCustomer != null)
            {
                using (var cc = new CustomersContext())
                {
                    Customer customer = cc.Customers.FirstOrDefault(c => c.Id == id);
                    if (!updateNulls)
                    {
                        customer.Email = newCustomer.Email ?? customer.Email;
                        customer.PasswordHash = newCustomer.PasswordHash ?? customer.PasswordHash;
                        customer.FirstName = newCustomer.FirstName ?? customer.FirstName;
                        customer.LastName = newCustomer.LastName ?? customer.LastName;
                        customer.Age = newCustomer.Age ?? customer.Age;
                    }
                    else
                    {
                        newCustomer.Id = customer.Id;
                        newCustomer.PasswordHash = newCustomer.PasswordHash ?? customer.PasswordHash;
                        if (newCustomer.Email != null)
                        {
                            if (cc.Customers.FirstOrDefault(c => c.Email == newCustomer.Email) != null)
                            {
                                newCustomer.Email = customer.Email;
                            }
                        }
                        customer = newCustomer;
                    }
                    cc.SaveChanges();
                    return customer;
                }
            }
            return null;
        }

        public Customer UpdateCustomer(Guid id, bool updateNulls, string email = null, string passwordHash = null, string firstName = null, string lastName = null, int? age = null)
        {
            return UpdateCustomer(id, updateNulls, new Customer() { Email = email, PasswordHash = passwordHash, FirstName = firstName, LastName = lastName, Age = age });
        }
    }
}
