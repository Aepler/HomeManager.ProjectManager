using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models
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
        public string Description_Extra { get; set; }
        public string[] Description_Tax { get; set; }
        public decimal Tax { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public decimal Amount_Tax { get; set; }
        public decimal Amount_Gross { get; set; }
        public decimal Amount_Net { get; set; }
        public decimal? Amount_Extra { get; set; }
        public string[] Amount_TaxList { get; set; }
        public byte[] Invoice { get; set; }
        public string DataType { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int fk_TypeId { get; set; }
        [ForeignKey("fk_TypeId")]
        public Type Type { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int fk_CategoryId { get; set; }
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
