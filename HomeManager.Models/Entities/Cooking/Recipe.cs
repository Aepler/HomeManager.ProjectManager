using HomeManager.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Entities.Cooking
{
    public class Recipe
    {
        public Recipe()
        {
            Tags = new HashSet<Tag>();
            Ingredients = new HashSet<Ingredient>();
        } 

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public TimeSpan TotalTime { get; set; }

        public TimeSpan CookingTime { get; set; }

        public TimeSpan PreapearingTime { get; set; }

        public CookingDifficulty Difficulty { get; set; }

        public byte Stars { get; set; }

        public string Description { get; set; }

        public string[] Instructions { get; set; }

        public bool Public { get; set; }

        public bool Deleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        [Display(Name = "User")]
        public Guid fk_UserId { get; set; }

 
        public ICollection<Favorites> Favorites { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }


    }
}
