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
            this.Payments = new HashSet<Payments>();
            this.Types = new HashSet<Type>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool EndPoint { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public ICollection<Payments> Payments { get; set; }
        public ICollection<Type> Types { get; set; }
    }
}
