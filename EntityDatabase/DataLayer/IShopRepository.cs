using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.DataLayer
{
    public interface IShopRepository
    {
        Shop CreateShop(long? inn, string name, string address = null);
        Shop GetShop(Guid shopId);
        void DeleteShop(Guid shopId);
    }
}
