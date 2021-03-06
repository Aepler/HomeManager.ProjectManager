using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HomeManager.Models.Entities.Finance
{
    public class Category
    {
        public Category()
        {
            this.Payments = new HashSet<Payment>();
            this.Repeatings = new HashSet<Repeating>();
            this.Templates = new HashSet<Template>();
        }

        [Key]
        public Guid Id { get; set; }

        [Display(Name = "User")]
        public Guid? fk_UserId { get; set; }
        [ForeignKey("fk_UserId")]
        public User User { get; set; }

        [Required]
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<Repeating> Repeatings { get; set; }
        public ICollection<Template> Templates { get; set; }
    }
}
