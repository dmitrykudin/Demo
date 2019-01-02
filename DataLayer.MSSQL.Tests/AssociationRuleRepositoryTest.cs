using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityDatabase.DataLayer.MSSQL;
using EntityDatabase.EntityModels;
using System.Collections.Generic;

namespace DataLayer.MSSQL.Tests
{
    [TestClass]
    public class AssociationRuleRepositoryTest
    {
        [TestMethod]
        public void ShouldGetAssociationRules()
        {
            AssociationRuleRepository repo = new AssociationRuleRepository();
            Guid productId = new Guid("D309B217-BE53-E811-BFD6-001583C810FA");
            
            List<AssociationRule> rules = repo.GetRulesByProduct(productId);
        }
    }
}
