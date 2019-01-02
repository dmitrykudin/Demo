using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.EntityModels
{
    public class ProductClass
    {
        public ProductClass()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        public string ShortName { get; set; }

        public ProductClass ParentClass { get; set; }

        public List<ProductClass> ChildenClasses { get; set; }

        public List<Product> Products { get; set; }

        public List<ClassTag> ClassTags { get; set; }
    }
}
