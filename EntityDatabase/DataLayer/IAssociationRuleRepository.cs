using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.DataLayer
{
    interface IAssociationRuleRepository
    {
        AssociationRule CreateRule(AssociationRule associationRule);
        List<AssociationRule> CreateRules(List<AssociationRule> associationRules);

        List<AssociationRule> GetRulesByProduct(Guid productId);
        List<AssociationRule> GetRulesByPurchase(Guid purchaseId);

        void DeleteRule(Guid id);
    }
}
