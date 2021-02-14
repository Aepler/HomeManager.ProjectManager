using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.DataTableModels
{
    public class PaymentTemplateDataTableModel
    {
        public string Id { get; set; }

        public string Date { get; set; }

        public string Description { get; set; }

        public string Amount { get; set; }

        public string fk_TypeId { get; set; }

        public string Type { get; set; }

        public string fk_CategoryId { get; set; }

        public string Category { get; set; }
    }
}
