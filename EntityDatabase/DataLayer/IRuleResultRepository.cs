using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.DataLayer
{
    interface IRuleResultRepository
    {
        RuleResult CreateResult(RuleResult ruleResult);
    }
}
