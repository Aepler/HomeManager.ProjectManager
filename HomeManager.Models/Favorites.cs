using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models
{
    public class Favorites
    {
        public Favorites() => Recipes = new HashSet<Recipe>();

        [Key]
        public int Id { get; set; }

        [Display(Name = "User")]

        public Guid? fk_UserId { get; set; }

        [ForeignKey("fk_UserId")]

        public User User { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
