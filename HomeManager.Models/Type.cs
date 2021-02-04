using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models
{
    public class Type
    {
        public Type()
        {
            this.Payments = new HashSet<Payment>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string TaxType { get; set; }
        public int Deleted { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
