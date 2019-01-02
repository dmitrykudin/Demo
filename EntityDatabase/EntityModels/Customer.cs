using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.EntityModels
{
    public class Customer
    {
        public Customer()
        {
            Purchases = new List<Purchase>();
            ProductRatings = new List<ProductRating>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string PasswordHash { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual List<Purchase> Purchases { get; set; }

        public virtual List<ProductRating> ProductRatings { get; set; }
    }
}
