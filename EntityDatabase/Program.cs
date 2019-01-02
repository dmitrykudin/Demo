using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Compiled...");
            using (CustomersContext cc = new CustomersContext())
            {
                cc.Customers.Add(new Customer() { FirstName = "Dmitry", LastName = "Kudin", Email = "dim271096@gmail.com", Age = 21, PasswordHash = "123456" });
                cc.SaveChanges();
            }
        }
    }
}
