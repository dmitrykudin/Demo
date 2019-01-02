using EntityDatabase.DataLayer;
using EntityDatabase.DataLayer.MSSQL;
using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.AprioriAlgorithm
{
    public class Apriori
    {
        public List<AssociationRule> ProccessApriori(double minSupport, double minConfidence, List<Product> products, List<Purchase> purchases)
        {
            Dictionary<List<Product>, double> frequentItems = GetL1FrequentItems(minSupport, products, purchases);
            Dictionary<List<Product>, double> allFrequentItems = new Dictionary<List<Product>, double>();
            foreach (var item in frequentItems)
            {
                allFrequentItems.Add(item.Key, item.Value);
            }
            Dictionary<List<Product>, double> candidates = new Dictionary<List<Product>, double>();
            double purchasesCount = purchases.Count;

            do
            {
                candidates = GenerateCandidates(frequentItems, purchases);
                frequentItems = GetFrequentItems(candidates, minSupport, purchasesCount);
                foreach (var item in frequentItems)
                {
                    if (!allFrequentItems.ContainsKey(item.Key))
                    {
                        allFrequentItems.Add(item.Key, item.Value);
                    }                    
                }
            }
            while (candidates.Count != 0);
            HashSet<Rule> rules = GenerateRules(allFrequentItems);
            List<Rule> strongRules = GetStrongRules(minConfidence, rules, allFrequentItems);
            List<AssociationRule> AssociationRules = new List<AssociationRule>();
            foreach (var rule in strongRules)
            {
                AssociationRule associationRule = new AssociationRule();
                foreach (var cond in rule.xList)
                {
                    RuleCondition condition = new RuleCondition();
                    condition.ProductId = cond.Id;
                    associationRule.RuleConditions.Add(condition);
                }
                foreach (var res in rule.yList)
                {
                    RuleResult result = new RuleResult();
                    result.ProductId = res.Id;
                    associationRule.RuleResults.Add(result);
                }
                associationRule.Confidence = Convert.ToDecimal(rule.Confidence);
                AssociationRules.Add(associationRule);
            }
            List<AssociationRule> savedRules = SaveToDatabase(AssociationRules);
            return savedRules;
        }
        
        private Dictionary<List<Product>, double> GetL1FrequentItems(double minSupport, List<Product> products, List<Purchase> purchases)
        {
            Dictionary<List<Product>, double> frequentItems = new Dictionary<List<Product>, double>();
            double purchasesCount = purchases.Count;

            if (purchasesCount > 0)
            {
                foreach (var product in products)
                {
                    List<Product> candidate = new List<Product>();
                    candidate.Add(product);
                    double support = GetSupport(candidate, purchases);
                    if (support >= minSupport)
                    {
                        List<Product> temp = new List<Product>();
                        temp.Add(product);
                        frequentItems.Add(temp, support);
                    }
                }
            }
            return frequentItems;
        }
        
        private double GetSupport(List<Product> candidate, List<Purchase> purchases)
        {
            double occurencesNum = 0;
            foreach (var purchase in purchases)
            {
                if (CheckIfSubset(candidate, purchase))
                {
                    occurencesNum++;
                }
            }
            return occurencesNum / purchases.Count;
        }

        private bool CheckIfSubset(List<Product> candidate, Purchase purchase)
        {
            foreach (var item in candidate)
            {
                if (purchase.ProductItems.FirstOrDefault(i => i.ProductId == item.Id) == null)
                {
                    return false;
                }
            }
            return true;
        }

        private Dictionary<List<Product>, double> GenerateCandidates(Dictionary<List<Product>, double> frequentItems, List<Purchase> purchases)
        {
            Dictionary<List<Product>, double> candidates = new Dictionary<List<Product>, double>();

            for (int i = 0; i < frequentItems.Count - 1; i++)
            {
                List<Product> itemOne = frequentItems.ElementAt(i).Key;
                for (int j = i + 1; j < frequentItems.Count; j++)
                {
                    List<Product> itemTwo = frequentItems.ElementAt(j).Key;
                    List<Product> generatedCandidate = GenerateCandidate(itemOne, itemTwo);
                    if (generatedCandidate != null)
                    {
                        double support = GetSupport(generatedCandidate, purchases);
                        if (FindCandidate(candidates, generatedCandidate) == null && !candidates.ContainsKey(generatedCandidate))
                        {
                            candidates.Add(generatedCandidate, support);
                        }
                    }
                }
            }

            return candidates;
        }

        private List<Product> FindCandidate(Dictionary<List<Product>, double> candidates, List<Product> generatedCandidate)
        {
            bool isMatch = false;
            List<Product> foundCandidate = new List<Product>();
            foreach (var key in candidates.Keys)
            {
                isMatch = true;
                for (int k = 0; k < key.Count; k++)
                {
                    if (key[k] != generatedCandidate[k])
                    {
                        isMatch = false;
                        break;
                    }                    
                }
                if (isMatch)
                {
                    foundCandidate = key;
                    break;
                } 
            }
            return foundCandidate.Count == 0 ? null : foundCandidate;
        }

        private List<Product> GenerateCandidate(List<Product> itemOne, List<Product> itemTwo)
        {
            if (itemOne.Count == 1)
            {
                List<Product> temp = new List<Product>();
                temp.AddRange(itemOne);
                temp.AddRange(itemTwo);
                return temp;
            }
            else
            {
                List<Product> differences = itemTwo.Except(itemOne).ToList();
                if (differences.Count == 1)
                {
                    List<Product> temp = new List<Product>();
                    temp.AddRange(itemOne);
                    temp.AddRange(differences);
                    return temp;
                }
                return null;
            }
        }
        
        private Dictionary<List<Product>, double> GetFrequentItems(Dictionary<List<Product>, double> candidates, double minSupport, double purchasesCount)
        {
            Dictionary<List<Product>, double> frequentItems = new Dictionary<List<Product>, double>();

            foreach (var item in candidates)
            {
                if (item.Value >= minSupport)
                {
                    frequentItems.Add(item.Key, item.Value);
                }
            }

            return frequentItems;
        }
        
        private HashSet<Rule> GenerateRules(Dictionary<List<Product>, double> allFrequentItems)
        {
            HashSet<Rule> rulesList = new HashSet<Rule>();

            foreach (var item in allFrequentItems)
            {
                if (item.Key.Count > 1)
                {
                    var subsets = SubSetsOf(item.Key).ToList();
                    subsets.RemoveAt(subsets.Count - 1);

                    var subsetsList = subsets.Where(s => s.Count() <= item.Key.Count / 2);
                    foreach (var subset in subsetsList)
                    {
                        List<Product> xList = subset.ToList();
                        List<Product> yList = item.Key.Except(xList).ToList();
                        Rule rule = new Rule(xList, yList, 0, item.Key);

                        bool isMatch = false;
                        foreach (var r in rulesList)
                        {
                            isMatch = true;
                            if (r.xList.Count == rule.xList.Count && r.yList.Count == rule.yList.Count)
                            {
                                if (r.xList.Except(rule.xList).Count() == 0)
                                {
                                    foreach (var pr in rule.yList)
                                    {
                                        if (!r.yList.Contains(pr))
                                        {
                                            isMatch = false;
                                            break;
                                        }
                                    }
                                }
                                else isMatch = false;
                            }
                            else isMatch = false;
                            if (isMatch) break;
                        }

                        if (!rulesList.Contains(rule) && !isMatch)
                        {
                            rulesList.Add(rule);
                        }
                    }                    
                }
            }

            return rulesList;
        }


        private List<Rule> GetStrongRules(double minConfidence, HashSet<Rule> rules, Dictionary<List<Product>, double> allFrequentItems)
        {
            List<Rule> strongRules = new List<Rule>();

            foreach (Rule rule in rules)
            {                
                List<Product> temp1 = allFrequentItems.FirstOrDefault(i => i.Key == rule.Item).Key;
                AddStrongRule(rule, temp1, strongRules, minConfidence, allFrequentItems);
            }

            strongRules.Sort((x, y) => y.Confidence.CompareTo(x.Confidence));
            return strongRules;
        }

        private void AddStrongRule(Rule rule, List<Product> item, List<Rule> strongRules, double minConfidence, Dictionary<List<Product>, double> allFrequentItems)
        {
            List<Product> temp = FindCandidate(allFrequentItems, rule.xList);
            double confidence = allFrequentItems[item] / allFrequentItems[temp];

            if (confidence >= minConfidence)
            {
                Rule newRule = new Rule(rule.xList, rule.yList, confidence, item);
                strongRules.Add(newRule);
            }            
        }

        public static IEnumerable<IEnumerable<T>> SubSetsOf<T>(IEnumerable<T> source)
        {
            if (!source.Any())
                return Enumerable.Repeat(Enumerable.Empty<T>(), 1);

            var element = source.Take(1);

            var haveNots = SubSetsOf(source.Skip(1));
            var haves = haveNots.Select(set => element.Concat(set));

            return haves.Concat(haveNots);
        }

        public List<AssociationRule> SaveToDatabase(List<AssociationRule> associationRules)
        {
            IAssociationRuleRepository associationRuleRepository = new AssociationRuleRepository();
            IRuleConditionRepository ruleConditionRepository = new RuleConditionRepository();
            IRuleResultRepository ruleResultRepository = new RuleResultRepository();
            List<AssociationRule> createdAssociationRules = new List<AssociationRule>();

            foreach (var associationRule in associationRules)
            {
                AssociationRule createdRule = associationRuleRepository.CreateRule(associationRule);
                if (createdRule != null)
                {
                    foreach (var condition in associationRule.RuleConditions)
                    {
                        condition.RuleId = createdRule.Id;
                        RuleCondition createdCondition = ruleConditionRepository.CreateCondition(condition);
                        createdRule.RuleConditions.Add(createdCondition);
                    }

                    foreach (var result in associationRule.RuleResults)
                    {
                        result.RuleId = createdRule.Id;
                        RuleResult createdResult = ruleResultRepository.CreateResult(result);
                        createdRule.RuleResults.Add(createdResult);
                    }
                    createdAssociationRules.Add(createdRule);
                }                
            }
            return createdAssociationRules;
        }
    }


}
