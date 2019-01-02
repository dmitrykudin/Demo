using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.DataLayer
{
    public interface ICustomerRepository
    {
        #region CRUD

        Customer CreateCustomer(string email, string passwordHash, string firstName = null, string lastName = null, int? age = null);

        Customer GetCustomer(Guid id);
        Guid GetCustomerId(string email);
        Customer GetCustomer(string email);        

        void DeleteCustomer(Guid id);
        void DeleteCustomer(string email);

        Customer UpdateCustomer(Guid id, bool updateNulls, Customer newCustomer);
        Customer UpdateCustomer(Guid id, bool updateNulls, string email = null, string passwordHash = null, string firstName = null, string lastName = null, int? age = null);

        #endregion
    }
}
