using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.EntityModels
{
    public class Purchase
    {
        public Purchase()
        {
            ProductItems = new List<ProductItem>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid ShopId { get; set; }

        public DateTime Date { get; set; }

        public decimal PurchaseSum { get; set; }

        public Customer Customer { get; set; }

        public Shop Shop { get; set; }

        public List<ProductItem> ProductItems { get; set; }
    }
}
