﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HomeManager.Models
{
    public class Status
    {
        public Status()
        {
            this.Payments = new HashSet<Payment>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int Deleted { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
