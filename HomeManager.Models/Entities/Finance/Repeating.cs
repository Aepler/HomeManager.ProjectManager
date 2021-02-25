using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Entities.Finance
{
    public class Repeating
    {
        [Key]
        public int Id { get; set; }

        public int RepeatInterval { get; set; }
        public DateTime RepeatStart { get; set; }
        public DateTime? RepeatEnd { get; set; }

        [Required]
        [Display(Name = "User")]
        public Guid fk_UserId { get; set; }
        [ForeignKey("fk_UserId")]
        public User User { get; set; }

        public string Description { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal Amount_Gross { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal Amount_Net { get; set; }
        public int Tax { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal Amount_Tax { get; set; }
        public string[] Description_ExtraCosts { get; set; }
        public string[] Amount_ExtraCosts { get; set; }
        public string[] Description_TaxList { get; set; }
        public string[] TaxList { get; set; }
        public string[] Amount_TaxList { get; set; }
        public byte[] Invoice { get; set; }
        public string DataType { get; set; }
        public int fk_TypeId { get; set; }
        public int? fk_CategoryId { get; set; }
        public int fk_StatusId { get; set; }

        public bool Deleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
