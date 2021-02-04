﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Interfaces
{
    public interface IPayment_TemplateService
    {
        Payment_Template GetById(int id);
        ICollection<Payment_Template> GetAll();
        ICollection<Payment_Template> GetByType(int fk_TypeId);
        ICollection<Payment_Template> GetByCategory(int fk_CategoryId);
        bool Add(Payment_Template payment_Template);
        bool Update(Payment_Template payment_Template);
    }
}
