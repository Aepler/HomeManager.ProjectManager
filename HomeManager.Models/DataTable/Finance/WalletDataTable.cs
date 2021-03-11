using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.DataTable.Finance
{
    public class WalletDataTable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartBalance { get; set; }
        public string CurrentBalance { get; set; }
    }
}
