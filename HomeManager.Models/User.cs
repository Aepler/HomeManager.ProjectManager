using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models
{
    public class User : IdentityUser<Guid>
    {
        public bool darkMode { get; set; }

        public ICollection<Payments> Payments { get; set; }
        public ICollection<Payment_Template> Payment_Templates { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
