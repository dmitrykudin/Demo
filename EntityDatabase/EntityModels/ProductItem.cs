using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.EntityModels
{
    public class ProductItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal Quantity { get; set; }

        public decimal Sum { get; set; }

        public Guid ProductId { get; set; }

        public Guid PurchaseId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Purchase Purchase { get; set; }
    }
}
