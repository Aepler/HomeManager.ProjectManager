using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.DataTables.Finance
{
    public class UserDataTable
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; } 

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string PhoneNumber { get; set; }

        public string TwoFactorEnabled { get; set; }

        public string EmailConfirmed { get; set; }

        public string PhoneNumberConfirmed { get; set; }

        public string LockoutEnd { get; set; }

        public string ConcurrencyStamp { get; set; } 

        public string LockoutEnabled { get; set; }

        public string AccessFailedCount { get; set; }
    }
}
