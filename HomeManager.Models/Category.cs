using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeManager.Models
{
    public class Category
    {
        public Category()
        {
            this.Payments = new HashSet<Payment>();
            this.Payment_Templates = new HashSet<Payment_Template>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Deleted { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<Payment_Template> Payment_Templates { get; set; }
    }
}
