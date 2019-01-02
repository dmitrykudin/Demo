using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.EntityModels
{
    public class Tag
    {
        public Tag()
        {
            ClassTags = new List<ClassTag>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TagName { get; set; }

        public virtual List<ClassTag> ClassTags { get; set; }
    }
}
