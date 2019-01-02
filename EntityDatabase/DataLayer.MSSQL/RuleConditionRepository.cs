using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDatabase.EntityModels;

namespace EntityDatabase.DataLayer.MSSQL
{
    public class RuleConditionRepository : IRuleConditionRepository
    {
        public RuleCondition CreateCondition(RuleCondition ruleCondition)
        {
            using (var cc = new CustomersContext())
            {
                RuleCondition condition = cc.RuleConditions
                    .FirstOrDefault(c => c.ProductId == ruleCondition.ProductId && c.RuleId == ruleCondition.RuleId);
                if (condition == null)
                {
                    condition = cc.RuleConditions.Add(new RuleCondition()
                    {
                        ProductId = ruleCondition.ProductId,
                        RuleId = ruleCondition.RuleId
                    });
                    cc.SaveChanges();
                }
                return condition;
            }
        }
    }
}
