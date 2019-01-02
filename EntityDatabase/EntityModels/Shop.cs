using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.EntityModels
{
    public class Shop
    {
        public Shop()
        {
            Purchases = new List<Purchase>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public long Inn { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public virtual List<Purchase> Purchases { get; set; }
    }
}
