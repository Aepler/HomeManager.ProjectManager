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
    }
}
