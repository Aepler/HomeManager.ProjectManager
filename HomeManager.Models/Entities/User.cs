using HomeManager.Models.Entities.Cooking;
using HomeManager.Models.Entities.Finance;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.Models.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Darkmode { get; set; }

        public byte[] ProfilePictureData { get; set; }

        public string ProfilePictureDataType { get; set; }

        public Guid? CurrentWallet { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<Template> Templates { get; set; }
        public ICollection<Repeating> Repeatings { get; set; }
        public ICollection<Wallet> Wallets { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Type> Types { get; set; }
        public ICollection<Status> Statuses { get; set; }

        public ICollection<Tag> Tags { get; set; }
        public ICollection<Recipe> Recipes { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
