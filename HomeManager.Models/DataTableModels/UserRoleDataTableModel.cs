using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.DataTableModels
{
    public class UserRoleDataTableModel
    {
        public string UserId { get; set; }
        public string User { get; set; }
        public string RoleId { get; set; }
        public string Role { get; set; }

        public string Buttons { get; set; }
    }
}
