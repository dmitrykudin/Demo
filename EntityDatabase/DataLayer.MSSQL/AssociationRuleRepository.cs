using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using EntityDatabase.EntityModels;

namespace EntityDatabase.DataLayer.MSSQL
{
    public class AssociationRuleRepository : IAssociationRuleRepository
    {
        public AssociationRule CreateRule(AssociationRule associationRule)
        {
            using (var cc = new CustomersContext())
            {
                AssociationRule rule = new AssociationRule();
                List<AssociationRule> rules = cc.AssociationRules.ToList();
                bool found = false;
                foreach (var item in rules)
                {
                    if (associationRule.Equals(item))
                    {
                        rule = item;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    rule = cc.AssociationRules.Add(new AssociationRule()
                    {
                        Confidence = associationRule.Confidence
                    });
                    cc.SaveChanges();
                    return rule;
                }
                else
                {
                    rule.Confidence = associationRule.Confidence;
                    cc.SaveChanges();
                    return null;
                }
            }
        }

        public List<AssociationRule> GetRulesByProduct(Guid productId)
        {
            using (var cc = new CustomersContext())
            {
                List<RuleCondition> conditions = cc.RuleConditions.Where(c => c.ProductId == productId).ToList();
                List<AssociationRule> allRules = new List<AssociationRule>();
                foreach (var condition in conditions)
                {
                    List<AssociationRule> rules = cc.AssociationRules.Include(r => r.RuleResults.Select(rr => rr.Product)).Where(r => r.Id == condition.RuleId).ToList();
                    allRules.AddRange(rules);
                }
                return allRules;
            }
        }

        public List<AssociationRule> GetRulesByPurchase(Guid purchaseId)
        {
            //TODO
            throw new NotImplementedException();
        }

        public List<AssociationRule> CreateRules(List<AssociationRule> associationRules)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void DeleteRule(Guid id)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
