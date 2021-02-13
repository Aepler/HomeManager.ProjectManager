using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.ViewModels.Admin
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string Buttons { get; set; }
    }
}
