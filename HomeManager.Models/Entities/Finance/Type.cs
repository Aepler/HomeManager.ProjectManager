﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Entities.Finance
{
    public class Type
    {
        public Type()
        {
            this.Payments = new HashSet<Payment>();
            this.Payment_Templates = new HashSet<PaymentTemplate>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string EndTaxType { get; set; }
        public bool Debit { get; set; }
        public string[] ExtraInput { get; set; }
        [Required]
        [Display(Name = "End Status")]
        public int fk_StatusId { get; set; }
        [ForeignKey("fk_StatusId")]
        public Status Status { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        [Display(Name = "User")]
        public Guid? fk_UserId { get; set; }
        [ForeignKey("fk_UserId")]
        public User User { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<PaymentTemplate> Payment_Templates { get; set; }
    }
}