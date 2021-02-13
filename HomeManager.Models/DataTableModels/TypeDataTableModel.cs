using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.DataTableModels
{
    public class TypeDataTableModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EndTaxType { get; set; }
        public string Debit { get; set; }
        public string[] ExtraInput { get; set; }
        public string fk_StatusId { get; set; }
        public string Status { get; set; }

        public string Buttons { get; set; }
    }
}
