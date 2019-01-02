using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.EntityModels
{
    public class Product
    {
        public Product()
        {
            ProductItems = new List<ProductItem>();
            ProductRatings = new List<ProductRating>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0.0, 5.0)]
        public decimal? Rating { get; set; }

        public int ClassId { get; set; }

        public ProductClass ProductClass { get; set; }

        public List<ProductItem> ProductItems { get; set; }

        public List<ProductRating> ProductRatings { get; set; }

        public HashSet<RuleCondition> RuleConditions { get; set; }

        public ICollection<RuleResult> RuleResults { get; set; }
    }
}
