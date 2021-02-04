using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string User { get; set; }
        public System.DateTime Date { get; set; }
        public string Description { get; set; }
        public string Description_Extra { get; set; }
        public string[] Description_Tax { get; set; }
        public decimal[] Tax { get; set; }
        public decimal Amount { get; set; }
        public decimal[] Amount_Tax { get; set; }
        public decimal Amount_Gross { get; set; }
        public decimal Amount_Net { get; set; }
        public string Amount_Extra { get; set; }
        public string Invoice { get; set; }

        public int fk_TypeId { get; set; }
        [ForeignKey("fk_TypeId")]
        public Type Type { get; set; }

        public int fk_CategoryId { get; set; }
        [ForeignKey("fk_CategoryId")]
        public Category Category { get; set; }

        public int fk_StatusId { get; set; }
        [ForeignKey("fk_StatusId")]
        public Status Status { get; set; }

        public bool Deleted { get; set; }
    }
}
