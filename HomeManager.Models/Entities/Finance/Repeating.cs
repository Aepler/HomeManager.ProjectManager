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
        public Repeating()
        {
            this.Payments = new HashSet<Payment>();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid fk_UserId { get; set; }

        public int RepeatInterval { get; set; }
        public DateTime RepeatStart { get; set; }
        public DateTime? RepeatEnd { get; set; }

        public Guid fk_WalletId { get; set; }
        [ForeignKey("fk_WalletId")]
        public Wallet Wallet { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal GrossAmount { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal NetAmount { get; set; }

        public string TaxDescription { get; set; }
        public int TaxRate { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal TaxAmount { get; set; }

        public string[] ExtraCostDescription { get; set; }
        public string[] ExtraCostAmount { get; set; }

        public string[] DetailedTaxDescription { get; set; }
        public string[] DetailedTaxRate { get; set; }
        public string[] DetailedTaxAmount { get; set; }

        public byte[] InvoiceData { get; set; }

        public string InvoiceDataType { get; set; }

        public DateTime? WarrantyExpiryDate { get; set; }

        public Guid fk_TypeId { get; set; }
        [ForeignKey("fk_TypeId")]
        public Type Type { get; set; }

        public Guid? fk_CategoryId { get; set; }
        [ForeignKey("fk_CategoryId")]
        public Category Category { get; set; }

        public bool Deleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
