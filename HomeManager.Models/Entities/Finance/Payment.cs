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
        public int Id { get; set; }

        [Required]
        [Display(Name = "User")]
        public Guid fk_UserId { get; set; }
        [ForeignKey("fk_UserId")]
        public User User { get; set; }

        [Required]
        public System.DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Description for extra Costs")]
        public string Description_Extra { get; set; }

        [Display(Name = "Description for Tax")]
        public string[] Description_Tax { get; set; }

        public decimal Tax { get; set; }
        public string[] TaxList { get; set; }

        [Required]
        [Display(Name = "Total Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Total Tax")]
        public decimal Amount_Tax { get; set; }

        [Display(Name = "Amount Gross")]
        public decimal Amount_Gross { get; set; }

        [Display(Name = "Amount Net")]
        public decimal Amount_Net { get; set; }

        [Display(Name = "Amount")]
        public decimal? Amount_Extra { get; set; }

        [Display(Name = "Amount")]
        public string[] Amount_TaxList { get; set; }

        public byte[] Invoice { get; set; }

        public string DataType { get; set; }

        [Display(Name = "Payment Template")]
        public int? fk_TemplateId { get; set; }
        [ForeignKey("fk_TemplateId")]
        public PaymentTemplate Payment_Template { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int fk_TypeId { get; set; }
        [ForeignKey("fk_TypeId")]
        public Type Type { get; set; }

        [Display(Name = "Category")]
        public int? fk_CategoryId { get; set; }
        [ForeignKey("fk_CategoryId")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int fk_StatusId { get; set; }
        [ForeignKey("fk_StatusId")]
        public Status Status { get; set; }

        public bool Deleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
