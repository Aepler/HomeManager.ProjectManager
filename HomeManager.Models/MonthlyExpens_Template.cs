using System;
using System.Collections.Generic;
using System.Text;

namespace HomeManager.Models
{
    public class MonthlyExpens_Template
    {
        public int ID { get; set; }
        public string User { get; set; }
        public System.DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Invoice { get; set; }
        public string Active { get; set; }
    }
}
