using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.ViewModels.Admin
{
    public class UserRolesModel
    {
        public Guid UserId { get; set; }
        public string User { get; set; }
        public Guid RoleId { get; set; }
        public string Role { get; set; }
    }
}
