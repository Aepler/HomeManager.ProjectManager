using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Services.Interfaces
{
    public interface IPayment_TemplateService
    {
        Task<Payment_Template> GetById(int id);
        Task<ICollection<Payment_Template>> GetAll();
        Task<ICollection<Payment_Template>> GetByType(int fk_TypeId);
        Task<ICollection<Payment_Template>> GetByCategory(int fk_CategoryId);
        Task<bool> Add(Payment_Template payment_Template);
        Task<bool> Update(Payment_Template payment_Template);
    }
}
