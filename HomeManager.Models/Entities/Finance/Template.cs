﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HomeManager.Models.Entities.Finance
{
    public class Template
    {
        public Template()
        {
            this.Payments = new HashSet<Payment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public Guid fk_UserId { get; set; }
        [ForeignKey("fk_UserId")]
        public User User { get; set; }

        public System.DateTime Date { get; set; }
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Amount { get; set; }
        public string Invoice { get; set; }

        public DateTime? RepeatStart { get; set; }
        public DateTime? RepeatEnd { get; set; }
        public int? RepeatInterval { get; set; }

        public bool Deleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public int fk_TypeId { get; set; }
        [ForeignKey("fk_TypeId")]
        public Type Type { get; set; }

        public int? fk_CategoryId { get; set; }
        [ForeignKey("fk_CategoryId")]
        public Category Category { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}