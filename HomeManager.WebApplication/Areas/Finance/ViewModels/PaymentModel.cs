using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.WebApplication.Areas.Finance.ViewModels
{
    public class PaymentModel
    {
        public string Date { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string Amount_Gross { get; set; }
        public string Amount_Net { get; set; }
        public string Tax { get; set; }
        public string ExtraCosts { get; set; }
        public string TaxList { get; set; }  
        public string Invoice { get; set; }
        public string Template { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string AdvancedAmount { get; set; }
        public string AdvancedTaxList { get; set; }
        public string AddTax { get; set; }
        public string AdvancedExtraCosts { get; set; }
        public string AddExtraCost { get; set; }
    }
}
