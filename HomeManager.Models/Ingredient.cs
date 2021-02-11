using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models
{
    public class Ingredient
    {
        public Ingredient() => Recipes = new HashSet<Recipe>();

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Ammount { get; set; }

        public bool Deleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        [Display(Name = "User")]
        public Guid? fk_UserId { get; set; }
        [ForeignKey("fk_UserId")]
        public User User { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
