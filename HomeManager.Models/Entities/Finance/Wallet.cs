using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Entities.Finance
{
    public class Wallet
    {
        public Wallet()
        {
            this.Payments = new HashSet<Payment>();
            this.Repeatings = new HashSet<Repeating>();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid fk_UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public double StartBalance { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public double CurrentBalance { get; set; }
        public DateTime? BalanceUpdateDate { get; set; }

        public bool Deleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<Repeating> Repeatings { get; set; }
    }
}
