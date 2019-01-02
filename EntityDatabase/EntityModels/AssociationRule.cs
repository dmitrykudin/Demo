using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.EntityModels
{
    public class AssociationRule 
    {
        public AssociationRule()
        {
            RuleConditions = new List<RuleCondition>();
            RuleResults = new List<RuleResult>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Range(0.0, 1.0)]
        public decimal Confidence { get; set; }
        
        public virtual List<RuleCondition> RuleConditions { get; set; }

        public virtual List<RuleResult> RuleResults { get; set; }

        public override bool Equals(object obj)
        {
            AssociationRule rightRule = obj as AssociationRule;
            IEqualityComparer<RuleCondition> conditionEqualityComparer = new RuleConditionComparer();
            IEqualityComparer<RuleResult> resultEqualityComparer = new RuleResultComparer();

            List<RuleCondition> conditionDifference = RuleConditions.Except(rightRule.RuleConditions, conditionEqualityComparer).ToList();
            List<RuleResult> resultDifference = RuleResults.Except(rightRule.RuleResults, resultEqualityComparer).ToList();

            if (conditionDifference.Count == 0 && resultDifference.Count == 0)
            {
                conditionDifference = rightRule.RuleConditions.Except(RuleConditions, conditionEqualityComparer).ToList();
                resultDifference = rightRule.RuleResults.Except(RuleResults, resultEqualityComparer).ToList();

                if (conditionDifference.Count == 0 && resultDifference.Count == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return RuleConditions.Count + RuleResults.Count;
        }
    }
}
