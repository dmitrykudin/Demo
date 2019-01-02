using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDatabase.EntityModels;

namespace EntityDatabase.DataLayer.MSSQL
{
    public class ShopRepository : IShopRepository
    {
        public Shop CreateShop(long? inn, string name, string address = null)
        {
            if (inn != null)
            {
                using (var cc = new CustomersContext())
                {
                    Shop shop = cc.Shops.FirstOrDefault(s => s.Inn == inn && s.Address == address && s.Name == name);
                    if (shop == null)
                    {
                        Shop newShop = new Shop()
                        {
                            Inn = inn.GetValueOrDefault(),
                            Name = name,
                            Address = address
                        };
                        cc.Shops.Add(newShop);
                        cc.SaveChanges();
                        return newShop;
                    }
                    return shop;
                }
            }
            return null;
        }

        public void DeleteShop(Guid shopId)
        {
            //TODO
            throw new NotImplementedException();
        }

        public Shop GetShop(Guid shopId)
        {
            using (var cc = new CustomersContext())
            {
                Shop shop = cc.Shops.FirstOrDefault(s => s.Id == shopId);
                return shop;
            }
        }
    }
}
