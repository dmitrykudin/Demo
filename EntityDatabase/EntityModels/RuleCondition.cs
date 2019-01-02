﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.EntityModels
{
    public class RuleCondition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public Guid RuleId { get; set; }

        public Guid ProductId { get; set; }

        public virtual AssociationRule AssociationRule { get; set; }

        public virtual Product Product { get; set; }        
    }

    public class RuleConditionComparer : IEqualityComparer<RuleCondition>
    {
        public bool Equals(RuleCondition x, RuleCondition y)
        {
            if (ReferenceEquals(x, y)) return true;

            return x != null && y != null && x.ProductId.Equals(y.ProductId);
        }

        public int GetHashCode(RuleCondition obj)
        {
            return obj.ProductId.GetHashCode();
        }
    }
}
