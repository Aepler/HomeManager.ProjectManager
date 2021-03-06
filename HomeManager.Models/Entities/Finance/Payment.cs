using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Entities.Finance
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "User")]
        public Guid fk_UserId { get; set; }
        [ForeignKey("fk_UserId")]
        public User User { get; set; }

        [Display(Name = "Wallet")]
        public Guid fk_WalletId { get; set; }
        [ForeignKey("fk_WalletId")]
        public Wallet Wallet { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Total Amount")]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Amount { get; set; }

        [Display(Name = "Amount Gross")]
        [Column(TypeName = "decimal(9,2)")]
        public decimal GrossAmount { get; set; }

        [Display(Name = "Amount Net")]
        [Column(TypeName = "decimal(9,2)")]
        public decimal NetAmount { get; set; }

        [Display(Name = "Tax Descripton")]
        public string TaxDescription { get; set; }
        [Display(Name = "Tax Rate")]
        public int TaxRate { get; set; }
        [Display(Name = "Total Tax")]
        [Column(TypeName = "decimal(9,2)")]
        public decimal TaxAmount { get; set; }

        [Display(Name = "Description for extra cost")]
        public string[] ExtraCostDescription { get; set; }
        [Display(Name = "Amount")]
        public string[] ExtraCostAmount { get; set; }

        [Display(Name = "Description for Tax")]
        public string[] DetailedTaxDescription { get; set; }
        [Display(Name = "Tax Rate")]
        public string[] DetailedTaxRate { get; set; }
        [Display(Name = "Amount")]
        public string[] DetailedTaxAmount { get; set; }

        public byte[] InvoiceData { get; set; }

        public string InvoiceDataType { get; set; }

        [Display(Name = "Warranty Expiry Date")]
        public DateTime? WarrantyExpiryDate { get; set; }

        [Display(Name = "Repeating Payment")]
        public Guid? fk_RepeatingId { get; set; }
        [ForeignKey("fk_RepeatingId")]
        public Repeating Repeating { get; set; }

        [Display(Name = "Type")]
        public Guid fk_TypeId { get; set; }
        [ForeignKey("fk_TypeId")]
        public Type Type { get; set; }

        [Display(Name = "Category")]
        public Guid? fk_CategoryId { get; set; }
        [ForeignKey("fk_CategoryId")]
        public Category Category { get; set; }

        [Display(Name = "Status")]
        public Guid fk_StatusId { get; set; }
        [ForeignKey("fk_StatusId")]
        public Status Status { get; set; }

        public bool Deleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
