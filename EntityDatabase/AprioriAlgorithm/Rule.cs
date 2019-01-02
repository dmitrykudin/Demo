using EntityDatabase.EntityModels;
using System.Collections.Generic;

namespace EntityDatabase.AprioriAlgorithm
{
    public class Rule
    {
        public Rule(List<Product> x, List<Product> y, double c, List<Product> i)
        {
            xList = x;
            yList = y;
            Confidence = c;
            Item = i;
        }

        public List<Product> xList { get; set; }
        public List<Product> yList { get; set; }
        public List<Product> Item { get; set; }
        public double Confidence { get; set; }
    }
}