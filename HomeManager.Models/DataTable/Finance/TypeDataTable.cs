using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.DataTable.Finance
{
    public class TypeDataTable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EndTaxType { get; set; }
        public string Debit { get; set; }
        public string[] ExtraInput { get; set; }
        public string fk_StatusId { get; set; }
        public string Status { get; set; }
    }
}
