using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.DataLayer
{
    public interface IPurchaseRepository
    {
        Purchase CreatePurchase(Guid customerId, Guid shopId, DateTime date, decimal purchaseSum);
        Purchase GetUserPurchase(Guid purchaseId, Guid customerId);
        Purchase GetPurchase(Guid id);
        List<Purchase> GetAllPurchases();
        List<Purchase> GetUserPurchases(Guid userId);
        List<Purchase> GetUserPurchases(Guid userId, DateTime from, DateTime to);
    }
}
