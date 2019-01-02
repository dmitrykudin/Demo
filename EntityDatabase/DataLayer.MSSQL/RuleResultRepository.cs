using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDatabase.EntityModels;

namespace EntityDatabase.DataLayer.MSSQL
{
    public class RuleResultRepository : IRuleResultRepository
    {
        public RuleResult CreateResult(RuleResult ruleResult)
        {
            using (var cc = new CustomersContext())
            {
                RuleResult result = cc.RuleResults
                    .FirstOrDefault(c => c.ProductId == ruleResult.ProductId && c.RuleId == ruleResult.RuleId);
                if (result == null)
                {
                    result = cc.RuleResults.Add(new RuleResult()
                    {
                        ProductId = ruleResult.ProductId,
                        RuleId = ruleResult.RuleId
                    });
                    cc.SaveChanges();
                }
                return result;
            }
        }
    }
}
