using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.DataTableModels
{
    public class PaymentDataTableModel
    {
        public string Id { get; set; }
        public string fk_UserId { get; set; }
        public string User { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Description_Extra { get; set; }
        public string[] Description_Tax { get; set; }
        public string Tax { get; set; }
        public string Amount { get; set; }
        public string Amount_Tax { get; set; }
        public string Amount_Gross { get; set; }
        public string Amount_Net { get; set; }
        public string Amount_Extra { get; set; }
        public string[] Amount_TaxList { get; set; }
        public string Invoice { get; set; }
        public string DataType { get; set; }
        public string fk_TemplateId { get; set; }
        public string Template { get; set; }
        public string fk_TypeId { get; set; }
        public string Type { get; set; }
        public string fk_CategoryId { get; set; }
        public string Category { get; set; }
        public string fk_StatusId { get; set; }
        public string Status { get; set; }
    }
}
