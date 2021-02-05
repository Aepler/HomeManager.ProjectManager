using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeManager.Models
{
    public class Status
    {
        public Status()
        {
            this.Payments = new HashSet<Payment>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Deleted { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
